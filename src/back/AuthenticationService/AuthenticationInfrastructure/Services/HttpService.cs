
using AuthenticationInfrastructure.Interface.Service;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace AuthenticationInfrastructure.Services
{
    public class HttpService : IHttpService
    {
        private const string EmailSendUrl = "https://localhost:7103/api/Values";

        public  async Task<bool> SendEmailCode(string Email, int Code) {
            HttpClient client = new HttpClient();
            var requestObject = new 
            {
                userEmail = Email,
                code = Code
            
            };
            var json = JsonConvert.SerializeObject(requestObject);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync(EmailSendUrl, content);

            return true;
        }
    }
}
