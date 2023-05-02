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
        var closeSuccess = indicatorValues.TryGetValue(required[0], out var close);
        var maSuccess = indicatorValues.TryGetValue(required[1], out var ma);
        if (!closeSuccess || !maSuccess) return SentimentStrength.Neutral;
        return IndicatorAnalysers.MovingAverage(ma, close);
    }

    private static SentimentStrength GetOscillatorRecommendation(Indicator indicator,
        IReadOnlyDictionary<string, decimal> indicatorValues)
    {
        const decimal defaultValue = decimal.MinValue;
        
        var indicatorName = indicator.ToString();
        if (!Indicators.IndicatorDependencies.TryGetValue(indicator, out var dependencies)) return SentimentStrength.Neutral;
        
        var indicatorArgs = dependencies.Select(dep => indicatorValues.GetValueOrDefault(dep, defaultValue)).ToArray();
        if (indicatorArgs.Any(x => x is defaultValue)) return SentimentStrength.Neutral;
        if (indicatorArgs.Length != dependencies.Count) return SentimentStrength.Neutral;
        
        return Enum.Parse(typeof(Oscillator), indicatorName) switch
        {
            Oscillator.ADX => IndicatorAnalysers.Adx(indicatorArgs[0], indicatorArgs[1], indicatorArgs[2], indicatorArgs[3], indicatorArgs[4]),
            Oscillator.BBP => IndicatorAnalysers.Simple(indicatorArgs[0]),
            Oscillator.AO => IndicatorAnalysers.Ao(indicatorArgs[0], indicatorArgs[1], indicatorArgs[2]),
            Oscillator.CCI => IndicatorAnalysers.Cci(indicatorArgs[0], indicatorArgs[1]),
            Oscillator.MACD => IndicatorAnalysers.Macd(indicatorArgs[0], indicatorArgs[1]),
            Oscillator.MOM => IndicatorAnalysers.Mom(indicatorArgs[0], indicatorArgs[1]),
            Oscillator.RSI => IndicatorAnalysers.Rsi(indicatorArgs[0], indicatorArgs[1]),
            Oscillator.Stoch => IndicatorAnalysers.Stoch(indicatorArgs[0], indicatorArgs[1], indicatorArgs[2], indicatorArgs[3]),
            Oscillator.StochRSI => IndicatorAnalysers.Simple(indicatorArgs[0]),
            Oscillator.UO => IndicatorAnalysers.Simple(indicatorArgs[0]),
            Oscillator.WR => IndicatorAnalysers.Simple(indicatorArgs[0]),
            _ => 0,
        };

    }

    private static Type? CheckEnumType(Indicator val)
    {
        if (!Enum.IsDefined(typeof(Indicator), val)) return null;
        var isOscillator = Enum.TryParse(typeof(Oscillator), val.ToString(), out _);
        if (isOscillator) return typeof(Oscillator);
        var isMovingAverage = Enum.TryParse(typeof(MovingAverage), val.ToString(), out _);
        return isMovingAverage ? typeof(MovingAverage) : null;
    }
}