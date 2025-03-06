using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace StockProtectionProxy
{
  public class StockProxyService : IStockService
  {
    private readonly Dictionary<string, DateTime> _requestTimestamps = new Dictionary<string, DateTime>();
    private readonly Dictionary<string, (StockResponse stock, DateTime timestamp)> _cache = new();
    /// <summary>
    /// Checks if user is a "premium" user and returns relevent stock info.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="userType"></param>
    /// <returns>stockResponse</returns>
    public async Task<StockResponse> GetStockPrice(string symbol, string userType)
    {
      try
      {// Proxy Feature: Log requests.
        Console.WriteLine($"Log: User: {userType} Stock Symbol: {symbol} Date: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        RealStockService realStock = new RealStockService();
        StockResponse stockResponse = new StockResponse();

        if (userType == "premium")
        {
          stockResponse = await realStock.Premium(symbol, userType);
        }

        else
        {
          // Proxy Feature: Caching requests and retrieving recent requests.
          if (_cache.TryGetValue(symbol, out var cachedStock) && (DateTime.Now - cachedStock.timestamp).TotalDays < 1)
          {
            Console.WriteLine($"Log: Returning cached stock: {cachedStock.stock.Symbol}, Price: {cachedStock.stock.CurrentPrice}");
            return cachedStock.stock;
          }

          // Proxy Feature - Rate-Limit Logic: Checks if the free user has made a stock info request within the last 30 seconds.
          if (_requestTimestamps.TryGetValue(userType, out var lastRequest) &&
            (DateTime.Now - _requestTimestamps[userType]).TotalSeconds < 30)
          {
            Console.WriteLine("Sorry, our free service is only available once every 30 seconds.");
            return null;
          }
          _requestTimestamps[userType] = DateTime.Now;

          stockResponse = await realStock.Free(symbol, userType);
          _cache[symbol] = (stockResponse, DateTime.Now);
        }
        return stockResponse;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error fetching stock price: {ex.Message}");
        return null;
      }
    }
  }
}

