
using Newtonsoft.Json;
using System.Text;

namespace AuthenticationInfrastructure.Services
{
    public static class HttpService
    {
        public static async Task<bool> SendEmailCode(string Email, int Code) {
            HttpClient client = new HttpClient();
            string url = "https://localhost:7103/api/Values";
            var requestObject = new 
            {
                userEmail = "string",
                code = 0
            
            };
            var json = JsonConvert.SerializeObject(requestObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) 
            {
                Console.WriteLine($"Успешно: {responseString}"); 
            } 
            else 
            {
                Console.WriteLine($"Ошибка: {responseString}");
            }
            return true;
        }
    }
}
