using System.Text.Json;

namespace AuthenticationDomain.Models
{
    public class ErrorModel
    {
        public int StausCode {  get; set; }
        public string ErrorStr { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
