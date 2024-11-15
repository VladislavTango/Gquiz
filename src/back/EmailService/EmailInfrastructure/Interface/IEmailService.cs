namespace EmailInfrastructure.Interface
{
    public interface IEmailService
    {
        Task SendConfirmCode();
        Task GenerateMail();
    }
}
