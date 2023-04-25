using Aesir.TradingView.Client.Interfaces;
using Aesir.TradingView.Client.Models;
using Aesir.TradingView.Enums;

namespace Aesir.TradingView.Client;

public class TradingViewClient
{
    private readonly IHttpClientWrapper _httpClient;

    public TradingViewClient(IHttpClientWrapper client)
    {
        _httpClient = client;
    }

    internal async Task<SymbolSignals[]> GetSignals(
        TechnicalAnalysisInterval taInterval,
        IEnumerable<string> tickers,
        IReadOnlyList<string> indicators)
    {
        var interval = GetInterval(taInterval);
        var req = new TradingViewRequest
        {
            Symbols =
            {
                Tickers = tickers
            },
            Columns = indicators.Select(x => $"{x}{interval}")
        };
        var res = await MakeRequest(req);
        return res
            .Select(x =>
                new SymbolSignals
                {
                    Symbol = RemoveBeforeFirstColon(x.Symbol),
                    Signals = CreateIndicatorDictionary(x.Signals, indicators)
                }
            )
            .ToArray();
    }

    private async Task<SignalResponse[]> MakeRequest(TradingViewRequest req)
    {
        try
        {
            var res = await _httpClient.PostAsync(req);
            return res == null ? Array.Empty<SignalResponse>() : res.Data;
        }
        catch
        {
            return Array.Empty<SignalResponse>();
        }
    }

    private static string RemoveBeforeFirstColon(string str) => str[(str.IndexOf(':') + 1)..];

    private static string GetInterval(TechnicalAnalysisInterval interval) => interval switch
    {
        TechnicalAnalysisInterval.OneMinute => "|1",
        TechnicalAnalysisInterval.FiveMinutes => "|5",
        TechnicalAnalysisInterval.FifteenMinutes => "|15",
        TechnicalAnalysisInterval.ThirtyMinutes => "|30",
        TechnicalAnalysisInterval.OneHour => "|60",
        TechnicalAnalysisInterval.TwoHours => "|120",
        TechnicalAnalysisInterval.FourHours => "|240",
        TechnicalAnalysisInterval.OneDay => "",
        TechnicalAnalysisInterval.OneWeek => "|1W",
        TechnicalAnalysisInterval.OneMonth => "|1M",
        _ => throw new ArgumentOutOfRangeException(nameof(interval), interval, null)
    };

    private static Dictionary<string, decimal> CreateIndicatorDictionary(IReadOnlyList<decimal> data,
        IReadOnlyList<string> indicators)
    {
        if (data.Count != indicators.Count)
            throw new Exception("A requested indicator did not have a signal value returned.");

        var res = new Dictionary<string, decimal>();
        for (var i = 0; i < data.Count; i++)
        {
            res.Add(indicators[i], data[i]);
        }

        return res;
    }
}