using Aesir.TradingView.IndicatorAnalysis;
using Aesir.TradingView.Sentiment.Enums;

namespace Aesir.TradingView.Tests.IndicatorAnalysis;

public class AnalysersTests
{
    [Fact]
    public void MovingAverage_ShouldBuy_WhenMovingAverageLessThanClosePrice() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.MovingAverage(1, 100));
    [Fact]
    public void MovingAverage_ShouldSell_WhenMovingAverageGreaterThanClosePrice() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.MovingAverage(100, 1));
    [Fact]
    public void MovingAverage_ShouldBeNeutral_WhenMovingAverageEqualsClosePrice() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.MovingAverage(100, 100));
    [Fact]
    public void RSI_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Rsi(25, 20));
    [Fact]
    public void RSI_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Rsi(71, 80));
    [Fact]
    public void RSI_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Rsi(40, 20));
    [Fact]
    public void Stoch_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Stoch(19, 18, 18, 19));
    [Fact]
    public void Stoch_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Stoch(81, 82, 2, 1));
    [Fact]
    public void Stoch_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Stoch(50, 50, 50, 50));
    [Fact]
    public void CCI_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Cci(-101, -102));
    [Fact]
    public void CCI_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Cci(101, 102));
    [Fact]
    public void CCI_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Cci(101, 100));
    [Fact]
    public void ADX_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Adx(21, 21, 20, 20, 21));
    [Fact]
    public void ADX_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Adx(21, 20, 21, 21, 20));
    [Fact]
    public void ADX_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Adx(20, 20, 20, 20, 20));
    [Fact]
    public void AO_ShouldBuy_WhenFirstBuyCase() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Ao(1, -1, 0));
    [Fact]
    public void AO_ShouldBuy_WhenSecondBuyCase() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Ao(1.1M, 1, 1.1M));
    [Fact]
    public void AO_ShouldSell_WhenFirstSellCase() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Ao(-1, 1, 0));
    [Fact]
    public void AO_ShouldSell_WhenSecondSellCase() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Ao(-1.1M, -0.5M, -1.1M));
    [Fact]
    public void AO_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Ao(1, 1, 1));
    [Fact]
    public void AO_ShouldBeNeutral_WhenAoIsZero() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Ao(0, 1, -1));
    [Fact]
    public void AO_ShouldBeNeutral_WhenAo1IsZero() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Ao(1, 0, -1));
    [Fact]
    public void AO_ShouldBeNeutral_WhenAo2IsZero() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Ao(1, 1, 0));
    [Fact]
    public void AO_ShouldBeNeutral_WhenAoAndAo1AreZero() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Ao(0, 0, -1));
    [Fact]
    public void AO_ShouldBeNeutral_WhenAoAndAo2AreZero() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Ao(0, 1, 0));
    [Fact]
    public void AO_ShouldBeNeutral_WhenAo1AndAo2AreZero() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Ao(1, 0, 0));
    [Fact]
    public void MOM_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Mom(1, 0));
    [Fact]
    public void MOM_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Mom(0, 1));
    [Fact]
    public void MOM_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Mom(0, 0));
    [Fact]
    public void MACD_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Macd(1, 0));
    [Fact]
    public void MACD_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Macd(0, 1));
    [Fact]
    public void MACD_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Macd(0, 0));
    [Fact]
    public void BBBuy_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.BbBuy(0, 1));
    [Fact]
    public void BBBuy_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.BbBuy(1, 0));
    [Fact]
    public void BBBuy_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.BbBuy(0, 0));
    [Fact]
    public void BBSell_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.BbSell(1, 0));
    [Fact]
    public void BBSell_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.BbSell(0, 0));
    [Fact]
    public void PSAR_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Psar(0, 1));
    [Fact]
    public void PSAR_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Psar(1, 0));
    [Fact]
    public void PSAR_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Psar(0, 0));
    [Fact]
    public void Simple_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.Simple(1));
    [Fact]
    public void Simple_ShouldSell_WhenGivenSellCondition() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.Simple(-1));
    [Fact]
    public void Simple_ShouldBeNeutral_WhenNotBuyOrSell() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.Simple(0));
    [Fact]
    public void GetRecommendation_ShouldBuy_WhenGivenBuyCondition() => Assert.Equal(SentimentStrength.Buy, IndicatorAnalysers.GetRecommendation(0.6M));
    [Fact]
    public void GetRecommendation_ShouldBeNeutral_WhenFirstNeutral() => Assert.Equal(SentimentStrength.Neutral, IndicatorAnalysers.GetRecommendation(-0.09M));
    [Fact]
    public void GetRecommendation_ShouldBeSell_WhenFirstSell() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.GetRecommendation(-0.3M));
    [Fact] 
    public void GetRecommendation_ShouldBeSell_WhenAnythingElse() => Assert.Equal(SentimentStrength.Sell, IndicatorAnalysers.GetRecommendation(-99M));
}