using Newtonsoft.Json;

namespace HotelListing_webAPI.Models
{
    public class Errors
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
      
    }
}
