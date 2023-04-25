using Aesir.TradingView.Sentiment.Enums;

namespace Aesir.TradingView.Sentiment.Models;

public class SymbolSentiment
{
    /// <summary>
    /// Total number of oscillator BUY signals
    /// </summary>
    public List<SentimentStrength> Oscillators { get; set; } = new();

    public int OscillatorsStrongBuy => Oscillators.Count(x => x == SentimentStrength.StrongBuy);
    public int OscillatorsBuy => Oscillators.Count(x => x == SentimentStrength.Buy);
    public int OscillatorsNeutral => Oscillators.Count(x => x == SentimentStrength.Neutral);
    public int OscillatorsSell => Oscillators.Count(x => x == SentimentStrength.Sell);
    public int OscillatorsStrongSell => Oscillators.Count(x => x == SentimentStrength.StrongSell);

    /// <summary>
    /// Total number of moving average BUY signals
    /// </summary>
    public List<SentimentStrength> MovingAverages { get; set; } = new();
    
    public int MovingAveragesStrongBuy => MovingAverages.Count(x => x == SentimentStrength.StrongBuy);
    public int MovingAveragesBuy => MovingAverages.Count(x => x == SentimentStrength.Buy);
    public int MovingAveragesNeutral => MovingAverages.Count(x => x == SentimentStrength.Neutral);
    public int MovingAveragesSell => MovingAverages.Count(x => x == SentimentStrength.Sell);
    public int MovingAveragesStrongSell => MovingAverages.Count(x => x == SentimentStrength.StrongSell);
}