using Aesir.TradingView.Client;
using Aesir.TradingView.Client.Interfaces;
using Aesir.TradingView.Client.Models;
using Aesir.TradingView.Enums;
using Moq;

namespace Aesir.TradingView.Tests.Client;

public class TradingViewClientTests
{
    private static Mock<IHttpClientWrapper> CreateClient(
        Func<TradingViewResponse>? callback = null)
    {
        callback ??= () => new TradingViewResponse
        {
            Data = Array.Empty<SignalResponse>()
        };
        var client = new Mock<IHttpClientWrapper>();
        client.Setup(x => x.PostAsync(It.IsAny<TradingViewRequest>())).ReturnsAsync(callback);
        return client;
    }

    [Fact]
    public async Task GetSignals_ReturnsExpectedWhenValid()
    {
        var tickers = new[] { "BINANCE:BTCUSDT" };
        var indicators = new[] { "close", "EMA10" };
        var client = CreateClient(() => new TradingViewResponse
        {
            Data = new[]
            {
                new SignalResponse
                {
                    Symbol = "BINANCE:BTCUSDT",
                    Signals = new[] { 500M, 510M }
                }
            }
        });

        var tvClient = new TradingViewClient(client.Object);
        var res = await tvClient.GetSignals(TechnicalAnalysisInterval.OneMinute, tickers, indicators);

        Assert.Single(res);
        Assert.Equal("BTCUSDT", res.FirstOrDefault()?.Symbol);
        Assert.True(res.FirstOrDefault()?.Signals.ContainsKey("close"));
        Assert.True(res.FirstOrDefault()?.Signals.ContainsKey("EMA10"));
        Assert.Equal(500, res.FirstOrDefault()?.Signals["close"]);
        Assert.Equal(510, res.FirstOrDefault()?.Signals["EMA10"]);
    }

    [Fact]
    public async Task GetSignals_ReturnsEmptyWhenHttpIssue()
    {
        var tickers = new[] { "BINANCE:BTCUSDT" };
        var indicators = new[] { "close", "EMA10" };
        var client = CreateClient(() => throw new Exception());

        var tvClient = new TradingViewClient(client.Object);
        var res = await tvClient.GetSignals(TechnicalAnalysisInterval.OneMinute, tickers, indicators);

        Assert.Empty(res);
    }

    [Fact]
    public async Task GetSignals_ParsesAllIntervals()
    {
        var intervals = Enum.GetValues<TechnicalAnalysisInterval>();
        var tickers = new[] { "BINANCE:BTCUSDT" };
        var indicators = new[] { "close", "EMA10" };
        var client = CreateClient(() => new TradingViewResponse
        {
            Data = new[]
            {
                new SignalResponse
                {
                    Symbol = "BINANCE:BTCUSDT",
                    Signals = new[] { 500M, 510M }
                }
            }
        });

        var tvClient = new TradingViewClient(client.Object);

        foreach (var interval in intervals)
        {
            var res = await tvClient.GetSignals(interval, tickers, indicators);
            Assert.NotEmpty(res);
        }
    }

    [Fact]
    public async Task GetSignals_ThrowsOnInvalidEnum()
    {
        var tickers = new[] { "BINANCE:BTCUSDT" };
        var indicators = new[] { "close", "EMA10" };
        var client = CreateClient(() => new TradingViewResponse
        {
            Data = new[]
            {
                new SignalResponse
                {
                    Symbol = "BINANCE:BTCUSDT",
                    Signals = new[] { 500M, 510M }
                }
            }
        });

        var tvClient = new TradingViewClient(client.Object);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            await tvClient.GetSignals((TechnicalAnalysisInterval)10, tickers, indicators));
    }

    [Fact]
    public async Task GetSignals_ThrowsOnMissingIndicator()
    {
        var tickers = new[] { "BINANCE:BTCUSDT" };
        var indicators = new[] { "close", "test123" };
        var client = CreateClient(() => new TradingViewResponse
        {
            Data = new[]
            {
                new SignalResponse
                {
                    Symbol = "BINANCE:BTCUSDT",
                    Signals = new[] { 500M }
                }
            }
        });

        var tvClient = new TradingViewClient(client.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
            await tvClient.GetSignals(TechnicalAnalysisInterval.OneMinute, tickers, indicators));
    }
}