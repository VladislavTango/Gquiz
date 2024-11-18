using EmailInfrastructure.Interface;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace EmailInfrastructure.Services
{
    public class EmailSenderService : IEmailService
    {
        public bool SendConfirmCode(string mail, int code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gquiz", "turbovlat@gmail.com"));
            message.To.Add(new MailboxAddress("Creator Confirm Code", $"{mail}"));
            message.Subject = "XULE PALISH'?";

            message.Body = new TextPart("plain")
            {
                Text = @$"Код для подтверждения вашего аккаунта: {code}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("turbovlat@gmail.com", "zgff auce qazk duqp");

                client.SendAsync(message);
                client.Disconnect(true);
            }
            return true;
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