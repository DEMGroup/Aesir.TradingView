using Aesir.TradingView.Sentiment.Enums;

namespace Aesir.TradingView.IndicatorAnalysis;

/// <summary>
/// Contains all relevant methods for determining whether an indicator is a BUY/SELL/NEUTRAL
/// </summary>
internal static class IndicatorAnalysers
{
    internal static SentimentStrength MovingAverage(decimal movingAverage, decimal closePrice)
    {
        if (movingAverage < closePrice) return SentimentStrength.Buy;
        return movingAverage > closePrice ? SentimentStrength.Sell : SentimentStrength.Neutral;
    }

    internal static SentimentStrength Rsi(decimal rsi, decimal rsi1)
    {
        return rsi switch
        {
            < 30 when rsi1 < rsi => SentimentStrength.Buy,
            > 70 when rsi1 > rsi => SentimentStrength.Sell,
            _ => SentimentStrength.Neutral
        };
    }

    internal static SentimentStrength Stoch(decimal stochK, decimal stochD, decimal stochK1, decimal stochD1)
    {
        return stochK switch
        {
            < 20 when stochD < 20 && stochK > stochD && stochK1 < stochD1 => SentimentStrength.Buy,
            > 80 when stochD > 80 && stochK < stochD && stochK1 > stochD1 => SentimentStrength.Sell,
            _ => SentimentStrength.Neutral
        };
    }

    internal static SentimentStrength Cci(decimal cci20, decimal cci201)
    {
        return cci20 switch
        {
            < -100 when cci20 > cci201 => SentimentStrength.Buy,
            > 100 when cci20 < cci201 => SentimentStrength.Sell,
            _ => SentimentStrength.Neutral
        };
    }

    internal static SentimentStrength Adx(decimal adx, decimal adxPdi, decimal adxNdi, decimal adxPdi1,
        decimal adxNdi1)
    {
        return adx switch
        {
            > 20 when adxPdi1 < adxNdi1 && adxPdi > adxNdi => SentimentStrength.Buy,
            > 20 when adxPdi1 > adxNdi1 && adxPdi < adxNdi => SentimentStrength.Sell,
            _ => SentimentStrength.Neutral
        };
    }

    internal static SentimentStrength Ao(decimal ao, decimal ao1, decimal ao2)
    {
        switch (ao)
        {
            case > 0 when ao1 < 0:
            case > 0 when ao1 > 0 && ao > ao1 && ao2 > ao1:
                return SentimentStrength.Buy;
            case < 0 when ao1 > 0:
            case < 0 when ao1 < 0 && ao < ao1 && ao2 < ao1:
                return SentimentStrength.Sell;
            default:
                return SentimentStrength.Neutral;
        }
    }

    internal static SentimentStrength Mom(decimal mom, decimal mom1)
    {
        if (mom > mom1) return SentimentStrength.Buy;
        return mom < mom1 ? SentimentStrength.Sell : SentimentStrength.Neutral;
    }

    internal static SentimentStrength Macd(decimal macd, decimal signal)
    {
        if (macd > signal) return SentimentStrength.Buy;
        return macd < signal ? SentimentStrength.Sell : SentimentStrength.Neutral;
    }

    internal static SentimentStrength BbBuy(decimal close, decimal bblower)
    {
        if (close < bblower) return SentimentStrength.Buy;
        return close > bblower ? SentimentStrength.Sell : SentimentStrength.Neutral;
    }

    internal static SentimentStrength BbSell(decimal close, decimal bbUpper)
        => close > bbUpper ? SentimentStrength.Buy : SentimentStrength.Neutral;


    internal static SentimentStrength Psar(decimal psar, decimal open)
    {
        if (psar < open) return SentimentStrength.Buy;
        return psar > open ? SentimentStrength.Sell : SentimentStrength.Neutral;
    }

    internal static SentimentStrength Simple(decimal val)
        => val switch
        {
            1 => SentimentStrength.Buy,
            -1 => SentimentStrength.Sell,
            _ => SentimentStrength.Neutral
        };

    internal static SentimentStrength GetRecommendation(decimal val)
    {
        return val switch
        {
            > 0.5M => SentimentStrength.Buy,
            >= -0.1M => SentimentStrength.Neutral,
            >= -0.5M => SentimentStrength.Sell,
            _ => SentimentStrength.Sell
        };
    }
}