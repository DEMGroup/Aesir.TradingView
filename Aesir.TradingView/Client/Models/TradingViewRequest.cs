namespace Aesir.TradingView.Client.Models;

/// <summary>
/// Represents the body of a TradingView request
/// </summary>
public class TradingViewRequest
{
    /// <summary>
    /// The list of symbols to be sent to TradingView
    /// </summary>
    public TickerData Symbols { get; set; } = new TickerData();

    /// <summary>
    /// The indicators to be processed
    /// </summary>
    public IEnumerable<string> Columns { get; set; } = Enumerable.Empty<string>();
}

/// <summary>
/// Contains the exchange tickers to be checked
/// </summary>
public class TickerData
{
    /// <summary>
    /// Ticker symbols in the form of EXCHANGE:TICKER
    /// </summary>
    public IEnumerable<string> Tickers { get; set; } = Enumerable.Empty<string>();

    /// <summary>
    /// Required by the TradingView request body
    /// </summary>
    public QueryInfo Query { get; set; } = new ();
}

/// <summary>
/// Unknown required type
/// </summary>
public class QueryInfo
{
    /// <summary>
    /// Type array
    /// </summary>
    public string[] Types { get; set; } = Array.Empty<string>();
}