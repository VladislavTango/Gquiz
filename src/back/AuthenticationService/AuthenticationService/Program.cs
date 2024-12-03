using AuthenticationInfrastructure.AppContext;
using AuthenticationInfrastructure.Interface.Repository;
using AuthenticationInfrastructure.Interface.Service;
using AuthenticationInfrastructure.Repository;
using AuthenticationInfrastructure.Services;
using AuthenticationInfrastructure.Services.JWT;
using CommonShared.Middlewares;
using CommonShared.RegistrationServices;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddControllers(options => 
{
    options.Filters.Add<ResponseFilter>();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICreaterRepository, CreaterRepository>();
builder.Services.AddScoped<IMailRepository, MailRepository>();

builder.Services.AddSingleton<IJwtTokentService, JwtTokenService>();
builder.Services.AddSingleton(x => new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
});

builder.Services.AddTransient<IRabbit, SendCodeRabbit>();


builder.Services.AddAppContext();
builder.Services.JwtAuthentication();

builder.Services.AddMediatRServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
