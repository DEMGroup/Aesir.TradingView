using Aesir.TradingView.Enums;
using Aesir.TradingView.IndicatorAnalysis;

namespace Aesir.TradingView.Tests.IndicatorAnalysis;

public class IndicatorsTests
{
    [Fact]
    public void GetIndicators_ShouldReturnAllIndicators()
    {
        var expectedIndicators = Enum.GetValues(typeof(Indicator)).Cast<Indicator>();
        var actualIndicators = Indicators.GetIndicators();
        Assert.Equal(expectedIndicators, actualIndicators);
    }
    
    [Fact]
    public void GetIndicators_ReturnsAllIndicators()
    {
        var expectedIndicators = Enum.GetValues(typeof(Indicator)).Cast<Indicator>();
        var actualIndicators = Indicators.GetIndicators();

        Assert.Equal(actualIndicators, expectedIndicators);
    }

    [Fact]
    public void GetIndicators_ReturnsDependenciesForADX()
    {
        var expectedDependencies = new List<string> { "ADX", "ADX+DI", "ADX-DI", "ADX+DI[1]", "ADX-DI[1]" };
        var actualDependencies = Indicators.GetIndicators(Indicator.ADX);

        Assert.Equal(actualDependencies, expectedDependencies);
    }

    [Fact]
    public void GetIndicators_ReturnsEmptyListForInvalidIndicator()
    {
        var actualDependencies = Indicators.GetIndicators((Indicator)999);
        Assert.Empty(actualDependencies);
    }
    
      [Theory]
    [InlineData(Indicator.ADX, new string[] { "ADX", "ADX+DI", "ADX-DI", "ADX+DI[1]", "ADX-DI[1]" })]
    [InlineData(Indicator.AO, new string[] { "AO", "AO[1]", "AO[2]" })]
    [InlineData(Indicator.BBP, new string[] { "Rec.BBPower", "BBPower" })]
    [InlineData(Indicator.CCI, new string[] { "CCI20", "CCI20[1]" })]
    [InlineData(Indicator.EMA10, new string[] { "close", "EMA10" })]
    [InlineData(Indicator.EMA100, new string[] { "close", "EMA100" })]
    [InlineData(Indicator.EMA20, new string[] { "close", "EMA20" })]
    [InlineData(Indicator.EMA200, new string[] { "close", "EMA200" })]
    [InlineData(Indicator.EMA30, new string[] { "close", "EMA30" })]
    [InlineData(Indicator.EMA5, new string[] { "close", "EMA5" })]
    [InlineData(Indicator.EMA50, new string[] { "close", "EMA50" })]
    [InlineData(Indicator.HullMA9, new string[] { "Rec.HullMA9" })]
    [InlineData(Indicator.Ichimoku, new string[] { "Rec.Ichimoku" })]
    [InlineData(Indicator.MACD, new string[] { "MACD.macd", "MACD.signal" })]
    [InlineData(Indicator.MOM, new string[] { "Mom", "Mom[1]" })]
    [InlineData(Indicator.RSI, new string[] { "RSI", "RSI[1]" })]
    [InlineData(Indicator.SMA10, new string[] { "close", "SMA10" })]
    [InlineData(Indicator.SMA100, new string[] { "close", "SMA100" })]
    [InlineData(Indicator.SMA20, new string[] { "close", "SMA20" })]
    [InlineData(Indicator.SMA200, new string[] { "close", "SMA200" })]
    [InlineData(Indicator.SMA30, new string[] { "close", "SMA30" })]
    [InlineData(Indicator.SMA5, new string[] { "close", "SMA5" })]
    [InlineData(Indicator.SMA50, new string[] { "close", "SMA50" })]
    [InlineData(Indicator.Stoch, new string[] { "Stoch.K", "Stoch.D", "Stoch.K[1]", "Stoch.D[1]" })]
    [InlineData(Indicator.StochRSI, new string[] { "Rec.Stoch.RSI", "Stoch.RSI.K" })]
    [InlineData(Indicator.UO, new string[] { "Rec.UO", "UO" })]

    public void TestIndicatorDependencies(Indicator indicator, string[] expectedDependencies)
    {
        var deps = Indicators.GetIndicators(indicator).ToList();
        Assert.NotEmpty(deps);
        Assert.Equal(expectedDependencies.Length, deps.Count);
        foreach (var dependency in expectedDependencies)
        {
            Assert.Contains(dependency, deps);
        }
    }
}