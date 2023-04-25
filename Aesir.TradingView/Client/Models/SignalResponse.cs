using System.Text.Json.Serialization;

namespace Aesir.TradingView.Client.Models;

public class SignalResponse
{
    [JsonPropertyName("s")]
    public string Symbol { get; set; } = string.Empty;
    
    [JsonPropertyName("d")]
    public decimal[] Signals { get; set; } = Array.Empty<decimal>();
}