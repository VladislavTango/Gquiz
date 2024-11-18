namespace EmailInfrastructure.Interface
{
    public interface IEmailService
    {
        bool SendConfirmCode(string mail , int code);
    }
}
