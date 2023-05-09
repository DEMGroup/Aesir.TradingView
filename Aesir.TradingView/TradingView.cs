using System.Text.RegularExpressions;
using Aesir.TradingView.Client;
using Aesir.TradingView.Client.Interfaces;
using Aesir.TradingView.Client.Models;
using Aesir.TradingView.Enums;
using Aesir.TradingView.Sentiment;
using Aesir.TradingView.Sentiment.Models;

namespace Aesir.TradingView;

/// <summary>
/// All relevant TradingView methods
/// </summary>
public class TradingView : ITradingView
{
    private static readonly Regex CheckTickerFormat 
        = new(pattern: "[\\W\\s]+", options: RegexOptions.Compiled);
    
    private readonly TradingViewClient _client;

    private readonly string[] _recommendedIndicators = { "Recommend.Other", "Recommend.MA" };

    public TradingView(IHttpClientWrapper? clientWrapper = null) => _client = new TradingViewClient(clientWrapper ?? new HttpClientWrapper());

    /// <summary>
    /// Gets the TradingView analysis for multiple tickers and indicators
    /// </summary>
    /// <param name="tickers"></param>
    /// <param name="exchange"></param>
    /// <param name="interval"></param>
    /// <param name="indicators"></param>
    public async Task<Dictionary<string, SymbolSentiment>?> GetSentiment(
        List<string> tickers,
        Exchange exchange,
        TechnicalAnalysisInterval interval,
        List<Indicator> indicators)
    {
        EnforceTickerFormat(tickers);
        var res = await GetSignals(tickers, exchange, interval,
            indicators.SelectMany(IndicatorAnalysis.Indicators.GetIndicators));
        return res.Length == 0
            ? null
            : res.ToDictionary(x => x.Symbol, x => TradingViewAnalyser.GenerateAnalysis(x, indicators));
    }

    /// <summary>
    /// Gets the TradingView analysis for multiple tickers and indicators
    /// </summary>
    /// <param name="tickers"></param>
    /// <param name="exchange"></param>
    /// <param name="interval"></param>
    /// <param name="indicators"></param>
    public async Task<Dictionary<string, Dictionary<string, decimal>>?> GetAnalysis(
        List<string> tickers,
        Exchange exchange,
        TechnicalAnalysisInterval interval,
        IEnumerable<Indicator> indicators)
    {
        EnforceTickerFormat(tickers);
        var taList = indicators.SelectMany(IndicatorAnalysis.Indicators.GetIndicators).Distinct().ToList();
        return await GetAnalysis(tickers, exchange, interval, taList);
    }

    /// <summary>
    /// Gets the TradingView analysis for multiple tickers and indicators
    /// </summary>
    /// <param name="tickers"></param>
    /// <param name="exchange"></param>
    /// <param name="interval"></param>
    /// <param name="indicators"></param>
    public async Task<Dictionary<string, Dictionary<string, decimal>>?> GetAnalysis(
        IEnumerable<string> tickers,
        Exchange exchange,
        TechnicalAnalysisInterval interval,
        IEnumerable<string> indicators)
    {
        var res = await GetSignals(tickers, exchange, interval, indicators);
        return res.ToDictionary(x => x.Symbol, x => x.Signals);
    }

    private async Task<SymbolSignals[]> GetSignals(
        IEnumerable<string> tickers,
        Exchange exchange,
        TechnicalAnalysisInterval interval,
        IEnumerable<string> indicators)
    {
        var taList = indicators
            .Concat(_recommendedIndicators)
            .Distinct()
            .Where(x => !string.IsNullOrEmpty(x)).ToList();
        return await _client.GetSignals(interval, tickers.Select(x => SanitizeTicker(x, exchange)), taList);
    }

    private static string SanitizeTicker(string ticker, Exchange exchange)
        => $"{exchange.ToString().ToUpper()}:{ticker.ToUpper()}{(ticker.Contains("USDT") ? "" : "USDT")}";

    private static void EnforceTickerFormat(IEnumerable<string> tickers)
    {
        var invalidTickers = tickers.Where(x => CheckTickerFormat.Match(x).Success).ToList();
        if (invalidTickers.FirstOrDefault() is { } ticker)
            throw new ArgumentException($"{ticker} is in an incorrect format.");
    }
}