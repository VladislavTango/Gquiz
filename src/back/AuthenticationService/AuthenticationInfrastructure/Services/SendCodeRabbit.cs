using AuthenticationInfrastructure.Interface.Service;
using RabbitMQ.Client;
using System.Text;

namespace AuthenticationInfrastructure.Services
{
    public class SendCodeRabbit : IRabbit
    {
        private readonly ConnectionFactory _connectionFactory;

        public SendCodeRabbit(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> SendCodeAsync(string email, int code)
        {
            string message = $"{email}:{code}";
            var body = Encoding.UTF8.GetBytes(message);

            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "Email code",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );


            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "Email code",
                body: body);

            return true;
        }
    }
}
