using Aesir.TradingView.Enums;

namespace Aesir.TradingView.IndicatorAnalysis;

/// <summary>
/// Indicators require extra indicators to inform the decision, any relevant helpers for that are in this class
/// </summary>
internal static class Indicators
{
    private static readonly Dictionary<Indicator, List<string>> IndicatorDependencies = new ()
    {
        { Indicator.ADX, new List<string> { "ADX", "ADX+DI", "ADX-DI", "ADX+DI[1]", "ADX-DI[1]" } },
        { Indicator.AO, new List<string> { "AO", "AO[1]", "AO[2]" } },
        { Indicator.BBP, new List<string> { "Rec.BBPower", "BBPower" } },
        { Indicator.CCI, new List<string> { "CCI20", "CCI20[1]" } },
        { Indicator.EMA10, new List<string> { "close", "EMA10" } },
        { Indicator.EMA100, new List<string> { "close", "EMA100" } },
        { Indicator.EMA20, new List<string> { "close", "EMA20" } },
        { Indicator.EMA200, new List<string> { "close", "EMA200" } },
        { Indicator.EMA30, new List<string> { "close", "EMA30" } },
        { Indicator.EMA5, new List<string> { "close", "EMA5" } },
        { Indicator.EMA50, new List<string> { "close", "EMA50" } },
        { Indicator.HullMA9, new List<string> { "Rec.HullMA9" } },
        { Indicator.Ichimoku, new List<string> { "Rec.Ichimoku" } },
        { Indicator.MACD, new List<string> { "MACD.macd", "MACD.signal" } },
        { Indicator.MOM, new List<string> { "Mom", "Mom[1]" } },
        { Indicator.RSI, new List<string> { "RSI", "RSI[1]" } },
        { Indicator.SMA10, new List<string> { "close", "SMA10" } },
        { Indicator.SMA100, new List<string> { "close", "SMA100" } },
        { Indicator.SMA20, new List<string> { "close", "SMA20" } },
        { Indicator.SMA200, new List<string> { "close", "SMA200" } },
        { Indicator.SMA30, new List<string> { "close", "SMA30" } },
        { Indicator.SMA5, new List<string> { "close", "SMA5" } },
        { Indicator.SMA50, new List<string> { "close", "SMA50" } },
        { Indicator.Stoch, new List<string> { "Stoch.K", "Stoch.D", "Stoch.K[1]", "Stoch.D[1]" } },
        { Indicator.StochRSI, new List<string> { "Rec.Stoch.RSI", "Stoch.RSI.K" } },
        { Indicator.UO, new List<string> { "Rec.UO", "UO" } },
        { Indicator.VWMA, new List<string> { "Rec.VWMA" } },
        { Indicator.WR, new List<string> { "Rec.WR", "W.R" } }
    };

    /// <summary>
    /// Returns all available indicators
    /// </summary>
    /// <returns></returns>
    internal static IEnumerable<Indicator> GetIndicators() =>
        Enum.GetValues(typeof(Indicator)).Cast<Indicator>().ToList();

    /// <summary>
    /// Returns the required extra indicators for a given indicator
    /// </summary>
    /// <param name="indicator"></param>
    /// <returns></returns>
    internal static IEnumerable<string> GetIndicators(Indicator indicator)
        => !IndicatorDependencies.ContainsKey(indicator) ? Enumerable.Empty<string>() : IndicatorDependencies[indicator];
}