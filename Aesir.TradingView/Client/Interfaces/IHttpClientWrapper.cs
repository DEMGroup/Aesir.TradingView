using Aesir.TradingView.Client.Models;

namespace Aesir.TradingView.Client.Interfaces;

public interface IHttpClientWrapper
{
    Task<TradingViewResponse?> PostAsync<T>(T body);
}