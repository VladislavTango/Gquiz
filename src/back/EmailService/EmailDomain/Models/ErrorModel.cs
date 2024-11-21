using System.Text.Json;

namespace EmailDomain.Models
{
    public class ErrorModel
    {
        public int StausCode { get; set; }
        public string ErrorStr { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
