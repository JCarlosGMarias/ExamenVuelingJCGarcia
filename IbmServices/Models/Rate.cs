using Newtonsoft.Json;

namespace IbmServices.Models
{
    public class Rate : BaseEntity
    {
        public string From { get; set; }
        public string To { get; set; }
        [JsonProperty("Rate")]
        public decimal RateVal { get; set; }
    }
}
