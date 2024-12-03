using EmailDomain.Models;
using EmailInfrastructure.Interface;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailInfrastructure.Services
{
    public class Rabbit : BackgroundService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IEmailService _emailService;

        public Rabbit(ConnectionFactory connectionFactory, IEmailService emailService)
        {
            _connectionFactory = connectionFactory;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "Email code",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var inf = message.Split(":");
                await _emailService.SendConfirmCode(inf[0], Convert.ToInt32(inf[1]));

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: "Email code",
                autoAck: true,
                consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
