using Aesir.TradingView.Enums;

namespace Aesir.TradingView.Tests;

public class TradingViewTests
{
    [Fact]
    public async Task GetSentiment_ThrowsOnInvalidTickers()
    {
        var tickers = new[] { "BTC/USDT" };
        var tv = new TradingView();
        await Assert.ThrowsAsync<ArgumentException>(async () => await tv.GetSentiment(tickers.ToList(), Exchange.Binance, TechnicalAnalysisInterval.OneMinute, new List<Indicator>()
        {
            Indicator.Ichimoku
        }));
    }
    
    [Fact]
    public async Task GetAnalysis_ThrowsOnInvalidTickers()
    {
        var tickers = new[] { "BTC/USDT" };
        var tv = new TradingView();
        await Assert.ThrowsAsync<ArgumentException>(async () => await tv.GetAnalysis(tickers.ToList(), Exchange.Binance, TechnicalAnalysisInterval.OneMinute, new List<Indicator>()
        {
            Indicator.Ichimoku
        }));
    }
}