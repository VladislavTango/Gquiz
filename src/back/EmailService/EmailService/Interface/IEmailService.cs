namespace EmailInfrastructure.Interface
{
    public interface IEmailService
    {
        Task SendConfirmCode(string mail , int code);
    }
}
