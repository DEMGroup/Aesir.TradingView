namespace Aesir.TradingView.Client.Models;

public class SymbolSignals
{
    public required string Symbol { get; set; }
    public Dictionary<string, decimal> Signals { get; set; } = new ();
}