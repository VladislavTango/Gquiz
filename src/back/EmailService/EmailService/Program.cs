using CommonShared.Middlewares;
using CommonShared.RegistrationServices;
using EmailInfrastructure.Interface;
using EmailInfrastructure.Services;
using RabbitMQ.Client;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton(x => new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        });

        builder.Services.AddHostedService<Rabbit>();

        builder.Services.AddTransient<IEmailService, EmailSenderService>();


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ResponseFilter>();
        });

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}