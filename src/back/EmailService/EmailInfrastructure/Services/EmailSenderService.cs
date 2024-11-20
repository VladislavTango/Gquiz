using EmailInfrastructure.Interface;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace EmailInfrastructure.Services
{
    public class EmailSenderService : IEmailService
    {
        public async Task SendConfirmCode(string mail, int code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gquiz", "turbovlat@gmail.com"));
            message.To.Add(new MailboxAddress("Creator Confirm Code", $"{mail}"));
            message.Subject = "igorek)))))'?";

            message.Body = new TextPart("plain")
            {
                Text = @$"Код для подтверждения вашего аккаунта: {code}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                await client.AuthenticateAsync("turbovlat@gmail.com", "zgff auce qazk duqp");

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
