using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;
using Sprache;
using System.Reflection.Metadata.Ecma335;

namespace StockProtectionProxy
{
  public class RealStockService
  {
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _apiKey;

    /// <summary>
    /// Loads API
    /// </summary>
    /// <exception cref="Exception"></exception>
    public RealStockService()
    {
      Env.Load();
      _apiKey = Environment.GetEnvironmentVariable("FINNHUB_API_KEY") ?? throw new Exception("API Key not found");
    }

    /// <summary>
    /// Passess in stock symbol and users info. Retrieves all relevent stock data, including the current price.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="userType"></param>
    /// <returns>stockResponse</returns>
    public async Task<StockResponse> Premium(string symbol, string userType)
    {
      StockResponse stockResponse = new StockResponse();

      stockResponse.Symbol = symbol;
      stockResponse.Name = await FetchCompanyName(symbol);
      stockResponse.MarketClosePrice = await FetchStockPrices(symbol, 2);
      stockResponse.CurrentPrice = await FetchStockPrices(symbol, 1);
      stockResponse.OpenPrice = await FetchStockPrices(symbol, 3);
      stockResponse.HighPrice = await FetchStockPrices(symbol, 4);
      stockResponse.LowPrice = await FetchStockPrices(symbol, 5);

      return stockResponse;
    }

    /// <summary>
    /// Passess in stock symbol and users info. Retrieves yesterdays price.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="userType"></param>
    /// <returns>stockResponse</returns>
    public async Task<StockResponse> Free(string symbol, string userType)
    {
      StockResponse stockResponse = new StockResponse();

      stockResponse.Symbol = symbol;
      stockResponse.Name = await FetchCompanyName(symbol);
      stockResponse.MarketClosePrice = await FetchStockPrices(symbol, 2);

      return stockResponse;
    }

    /// <summary>
    /// Calls API to get the stock price data.
    /// </summary>
    /// <param name="stockSymbol"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task<double> FetchStockPrices(string stockSymbol, int v)
    {
      var url = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol.ToUpper()}&token={_apiKey}";
      var response = await _httpClient.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception($"Failed to fetch stock price information for {stockSymbol.ToUpper()}");
      }

      var content = await response.Content.ReadAsStringAsync();
      var stockData = JsonConvert.DeserializeObject<StockData>(content);

      switch (v)
      {
        case 1:
          return stockData?.CurrentPrice ?? throw new Exception("Failed to parse current stock price.");
        case 2:
          return stockData?.PreviousClosePrice ?? throw new Exception("Failed to parse the previous day's stock price.");
        case 3:
          return stockData?.OpenPrice ?? throw new Exception("Failed to parse the opening stock price.");
        case 4:
          return stockData?.HighPrice ?? throw new Exception("Failed to parse the current day's high stock price.");
        case 5:
          return stockData?.LowPrice ?? throw new Exception("Failed to parse the current day's low stock price.");
        default:
          throw new Exception("Invalid option for FetchStockPrices.");
      }
    }

    /// <summary>
    /// Fetches company name that matches entered stock symbol.
    /// </summary>
    /// <param name="stockSymbol"></param>
    /// <returns>company name</returns>
    private async Task<string> FetchCompanyName(string stockSymbol)
    {
      var lines = await File.ReadAllLinesAsync("constituents.csv");

      foreach (var line in lines.Skip(1))
      {
        var data = line.Split(',');
        
        if (data.Length >= 2 && data[0].Trim().Equals(stockSymbol, StringComparison.OrdinalIgnoreCase))
        {
          return data[1].Trim();
        }
      }

      return "Unknown Company";
    }
  }
}
