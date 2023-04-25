namespace Aesir.TradingView.Client.Models;

public class TradingViewResponse
{
    public SignalResponse[] Data { get; set; } = Array.Empty<SignalResponse>();
}