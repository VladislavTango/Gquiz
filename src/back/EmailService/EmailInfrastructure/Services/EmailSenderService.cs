using EmailInfrastructure.Interface;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace EmailInfrastructure.Services
{
    public class EmailSenderService : IEmailService
    {
        public Task GenerateMail(string mail , )
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Nasral", "turbovlat@gmail.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "zalarderon00@mail.ru"));
            message.Subject = "XULE PALISH'?";

            message.Body = new TextPart("plain")
            {
                Text = @"ди нахуй"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("turbovlat@gmail.com", "zgff auce qazk duqp");

                client.Send(message);
                client.Disconnect(true);
            }
            throw new Exception("da");
        }
        public Task SendConfirmCode()
        {
            throw new NotImplementedException();
        }
    }
}


//syntax = "proto3";


//package MailProto

//message ConfirmCodeRequest
//{
//	string email = 1;
//string code = 2; 
//}

//message ConfirmCodeResponse
//{
//	bool success = 1;
//	string message = 2;
//}

//service EmailServiceProto
//{
//    rpc ConfirmCode (ConfirmCodeRequest) returns (ConfirmCodeResponse);
//}