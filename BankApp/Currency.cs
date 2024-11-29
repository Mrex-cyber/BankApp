using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankApp
{
    public class Currency
    {
        [JsonPropertyName("currencyCodeA")] 
        public int CurrencyCodeA { get; set; }
        [JsonPropertyName("currencyCodeB")] 
        public int CurrencyCodeB { get; set; }
        [JsonPropertyName("date")] 
        public long Date { get; set; }
        [JsonPropertyName("rateBuy")] 
        public double RateBuy { get; set; }
        [JsonPropertyName("rateSell")]
        public double RateSell { get; set; }
    }
}
