using Aesir.TradingView.Enums;
using Aesir.TradingView.Sentiment.Models;

namespace Aesir.TradingView;

public interface ITradingView
{
    Task<Dictionary<string, Dictionary<string, decimal>>?> GetAnalysis(
        List<string> tickers,
        Exchange exchange,
        TechnicalAnalysisInterval interval,
        IEnumerable<Indicator> indicators);

    Task<Dictionary<string, SymbolSentiment>?> GetSentiment(
        List<string> tickers,
        Exchange exchange,
        TechnicalAnalysisInterval interval,
        List<Indicator> indicators);

    Task<Dictionary<string, Dictionary<string, decimal>>?> GetAnalysis(
        IEnumerable<string> tickers,
        Exchange exchange,
        TechnicalAnalysisInterval interval,
        IEnumerable<string> indicators);
}