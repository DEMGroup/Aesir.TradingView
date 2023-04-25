using System.Text.Encodings.Web;
using System.Text.Json;
using Aesir.TradingView.Client.Interfaces;
using Aesir.TradingView.Client.Models;

namespace Aesir.TradingView.Client;

public class HttpClientWrapper : IHttpClientWrapper
{
    private const string ScannerUrl = "https://scanner.tradingview.com/crypto/scan";
    
    private readonly HttpClient _httpClient;

    public HttpClientWrapper()
    {
        _httpClient = new HttpClient
        {
            DefaultRequestHeaders =
            {
                { "User-Agent", "tradingview_ta/3.2.10" }
            }
        };
    }

    public async Task<TradingViewResponse?> PostAsync<T>(T body)
    {
        var opts = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        var res = await _httpClient.PostAsync(ScannerUrl, new StringContent(JsonSerializer.Serialize(body, opts)));
        
        var content = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TradingViewResponse>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}