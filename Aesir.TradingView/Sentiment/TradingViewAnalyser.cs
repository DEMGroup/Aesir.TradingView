using Aesir.TradingView.Client.Models;
using Aesir.TradingView.Enums;
using Aesir.TradingView.IndicatorAnalysis;
using Aesir.TradingView.Sentiment.Enums;
using Aesir.TradingView.Sentiment.Models;

namespace Aesir.TradingView.Sentiment;

/// <summary>
/// Contains all relevant methods for parsing TA results
/// </summary>
public static class TradingViewAnalyser
{
    internal static SymbolSentiment GenerateAnalysis(SymbolSignals signals, List<Indicator> indicators)
        => GetRecommendations(signals.Signals, indicators);

    private static SymbolSentiment GetRecommendations(IReadOnlyDictionary<string, decimal> dict,
        List<Indicator> indicators)
    {
        var res = new SymbolSentiment();

        foreach (var indicator in indicators)
        {
            if (indicator is not (Indicator.Ichimoku or Indicator.VWMA or Indicator.HullMA9))
            {
                var enumType = CheckEnumType(indicator);
                if (enumType == null) continue;
                if (enumType == typeof(MovingAverage))
                    res.MovingAverages.Add(GetMovingAverageRecommendation(indicator, dict));
                else if (enumType == typeof(Oscillator))
                    res.Oscillators.Add(GetOscillatorRecommendation(indicator, dict));
            }
            else if (dict.TryGetValue($"Rec.{indicator}", out var recommendedValue))
                res.MovingAverages.Add(IndicatorAnalysers.Simple(recommendedValue));
        }

        if (dict.TryGetValue("Recommend.Other", out var otherRecommendedValue))
            res.Oscillators.Add(IndicatorAnalysers.GetRecommendation(otherRecommendedValue));
        if (dict.TryGetValue("Recommend.MA", out var maRecommendedValue))
            res.MovingAverages.Add(IndicatorAnalysers.GetRecommendation(maRecommendedValue));

        return res;
    }

    private static SentimentStrength GetMovingAverageRecommendation(Indicator indicator,
        IReadOnlyDictionary<string, decimal> indicatorValues)
    {
        var required = Indicators.GetIndicators(indicator).ToArray();
        var close = indicatorValues[required[0]];
        var ma = indicatorValues[required[1]];
        return IndicatorAnalysers.MovingAverage(ma, close);
    }

    private static SentimentStrength GetOscillatorRecommendation(Indicator indicator,
        IReadOnlyDictionary<string, decimal> indicatorValues)
        => Enum.Parse(typeof(Oscillator), indicator.ToString()) switch
        {
            Oscillator.ADX => IndicatorAnalysers.Adx(indicatorValues["ADX"], indicatorValues["ADX+DI"],
                indicatorValues["ADX-DI"], indicatorValues["ADX+DI[1]"], indicatorValues["ADX-DI[1]"]),
            Oscillator.BBP => IndicatorAnalysers.Simple(indicatorValues["Rec.BBPower"]),
            Oscillator.AO => IndicatorAnalysers.Ao(indicatorValues["AO"], indicatorValues["AO[1]"],
                indicatorValues["AO[2]"]),
            Oscillator.CCI => IndicatorAnalysers.Cci(indicatorValues["CCI20"], indicatorValues["CCI20[1]"]),
            Oscillator.MACD => IndicatorAnalysers.Macd(indicatorValues["MACD.macd"], indicatorValues["MACD.signal"]),
            Oscillator.MOM => IndicatorAnalysers.Mom(indicatorValues["Mom"], indicatorValues["Mom[1]"]),
            Oscillator.RSI => IndicatorAnalysers.Rsi(indicatorValues["RSI"], indicatorValues["RSI[1]"]),
            Oscillator.Stoch => IndicatorAnalysers.Stoch(indicatorValues["Stoch.K"], indicatorValues["Stoch.D"],
                indicatorValues["Stoch.K[1]"], indicatorValues["Stoch.D[1]"]),
            Oscillator.StochRSI => IndicatorAnalysers.Simple(indicatorValues["Rec.Stoch.RSI"]),
            Oscillator.UO => IndicatorAnalysers.Simple(indicatorValues["Rec.UO"]),
            Oscillator.WR => IndicatorAnalysers.Simple(indicatorValues["Rec.WR"]),
            _ => 0,
        };

    private static Type? CheckEnumType(Indicator val)
    {
        if (!Enum.IsDefined(typeof(Indicator), val)) return null;
        var isOscillator = Enum.TryParse(typeof(Oscillator), val.ToString(), out _);
        if (isOscillator) return typeof(Oscillator);
        var isMovingAverage = Enum.TryParse(typeof(MovingAverage), val.ToString(), out _);
        return isMovingAverage ? typeof(MovingAverage) : null;
    }
}