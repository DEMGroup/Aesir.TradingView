using Aesir.TradingView.Client.Models;
using Aesir.TradingView.Enums;
using Aesir.TradingView.Sentiment;

namespace Aesir.TradingView.Tests.Sentiment;

public class TradingViewAnalyserTests
{
    [Fact]
    public void GenerateAnalysis_ReturnsCorrectSentiment_ForOscillator()
    {
        var signals = new SymbolSignals { 
            Symbol = "BTCUSDT",
            Signals = new Dictionary<string, decimal> { 
                { "MACD.macd", 1 }, 
                { "MACD.signal", 2 }
            }
        };
        var indicators = new List<Indicator> { 
            Indicator.MACD
        };

        var sentiment = TradingViewAnalyser.GenerateAnalysis(signals, indicators);

        Assert.Single(sentiment.Oscillators);
        
        Assert.Equal(0, sentiment.MovingAveragesStrongBuy);
        Assert.Equal(0, sentiment.MovingAveragesBuy);
        Assert.Equal(0, sentiment.MovingAveragesNeutral);
        Assert.Equal(0, sentiment.MovingAveragesSell);
        Assert.Equal(0, sentiment.MovingAveragesStrongSell);

        Assert.Equal(0, sentiment.OscillatorsStrongBuy);
        Assert.Equal(0, sentiment.OscillatorsBuy);
        Assert.Equal(0, sentiment.OscillatorsNeutral);
        Assert.Equal(1, sentiment.OscillatorsSell);
        Assert.Equal(0, sentiment.OscillatorsStrongSell);
    }
    
    [Fact]
    public void GenerateAnalysis_ReturnsNothing_ForInvalidIndicator()
    {
        var signals = new SymbolSignals { 
            Symbol = "BTCUSDT",
            Signals = new Dictionary<string, decimal> { 
                { "MACD.macd", 1 }, 
                { "MACD.signal", 2 }
            }
        };
        var indicators = new List<Indicator> { 
            (Indicator)999
        };

        var sentiment = TradingViewAnalyser.GenerateAnalysis(signals, indicators);

        Assert.Empty(sentiment.Oscillators);
        Assert.Empty(sentiment.MovingAverages);
    }
    
    [Fact]
    public void GenerateAnalysis_ReturnsCorrectSentiment_ForMovingAverage()
    {
        var signals = new SymbolSignals { 
            Symbol = "BTCUSDT",
            Signals = new Dictionary<string, decimal> { 
                { "close", 100 }, 
                { "EMA10", 2 }
            }
        };
        var indicators = new List<Indicator> { 
            Indicator.EMA10
        };

        var sentiment = TradingViewAnalyser.GenerateAnalysis(signals, indicators);

        Assert.Single(sentiment.MovingAverages);
        
        Assert.Equal(0, sentiment.MovingAveragesStrongBuy);
        Assert.Equal(1, sentiment.MovingAveragesBuy);
        Assert.Equal(0, sentiment.MovingAveragesNeutral);
        Assert.Equal(0, sentiment.MovingAveragesSell);
        Assert.Equal(0, sentiment.MovingAveragesStrongSell);

        Assert.Equal(0, sentiment.OscillatorsStrongBuy);
        Assert.Equal(0, sentiment.OscillatorsBuy);
        Assert.Equal(0, sentiment.OscillatorsNeutral);
        Assert.Equal(0, sentiment.OscillatorsSell);
        Assert.Equal(0, sentiment.OscillatorsStrongSell);
    }
    
    [Fact]
    public void GenerateAnalysis_ReturnsCorrectSentiment_ForOther()
    {
        var signals = new SymbolSignals { 
            Symbol = "BTCUSDT",
            Signals = new Dictionary<string, decimal> { 
                { "Rec.Ichimoku", 1 }
            }
        };
        var indicators = new List<Indicator> { 
            Indicator.Ichimoku
        };

        var sentiment = TradingViewAnalyser.GenerateAnalysis(signals, indicators);

        Assert.Single(sentiment.MovingAverages);
        
        Assert.Equal(0, sentiment.MovingAveragesStrongBuy);
        Assert.Equal(1, sentiment.MovingAveragesBuy);
        Assert.Equal(0, sentiment.MovingAveragesNeutral);
        Assert.Equal(0, sentiment.MovingAveragesSell);
        Assert.Equal(0, sentiment.MovingAveragesStrongSell);

        Assert.Equal(0, sentiment.OscillatorsStrongBuy);
        Assert.Equal(0, sentiment.OscillatorsBuy);
        Assert.Equal(0, sentiment.OscillatorsNeutral);
        Assert.Equal(0, sentiment.OscillatorsSell);
        Assert.Equal(0, sentiment.OscillatorsStrongSell);
    }
    
    [Fact]
    public void GenerateAnalysis_ReturnsCorrectSentiment_ForRecommended()
    {
        var signals = new SymbolSignals { 
            Symbol = "BTCUSDT",
            Signals = new Dictionary<string, decimal> { 
                { "Recommend.Other", 1 },
                { "Recommend.MA", 1 }
            }
        };
        var indicators = new List<Indicator> { 
            
        };

        var sentiment = TradingViewAnalyser.GenerateAnalysis(signals, indicators);

        Assert.Single(sentiment.MovingAverages);
        Assert.Single(sentiment.Oscillators);
        
        Assert.Equal(0, sentiment.MovingAveragesStrongBuy);
        Assert.Equal(1, sentiment.MovingAveragesBuy);
        Assert.Equal(0, sentiment.MovingAveragesNeutral);
        Assert.Equal(0, sentiment.MovingAveragesSell);
        Assert.Equal(0, sentiment.MovingAveragesStrongSell);

        Assert.Equal(0, sentiment.OscillatorsStrongBuy);
        Assert.Equal(1, sentiment.OscillatorsBuy);
        Assert.Equal(0, sentiment.OscillatorsNeutral);
        Assert.Equal(0, sentiment.OscillatorsSell);
        Assert.Equal(0, sentiment.OscillatorsStrongSell);
    }
}