using AuthenticationApplication;
using AuthenticationInfrastructure.AppContext;
using AuthenticationInfrastructure.Interface;
using AuthenticationInfrastructure.Repository;
using AuthenticationInfrastructure.Services.JWT;
using AuthenticationService.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICreaterRepository, CreaterRepository>();
builder.Services.AddScoped<IMailRepository, MailRepository>();

builder.Services.AddScoped<IJwtTokentService, JwtTokenService>();

builder.Services.AddMediatRServices();
builder.Services.AddAppContext();
builder.Services.JwtAuthentication();



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
