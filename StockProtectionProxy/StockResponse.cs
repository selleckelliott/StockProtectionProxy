using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockProtectionProxy
{
  public class StockResponse
  {
    /// <summary>
    /// Gets or sets the stock symbol.
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the company name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the price of a stock at the market close of the previous day.
    /// </summary>
    public double MarketClosePrice { get; set; }

    /// <summary>
    /// Gets or sets the current stock price.
    /// </summary>
    public double CurrentPrice { get; set; }

    /// <summary>
    /// Gets or sets the opening price of the day.
    /// </summary>
    public double OpenPrice { get; set; }

    /// <summary>
    /// Gets or sets the high price of the day.
    /// </summary>
    public double HighPrice { get; set; }

    /// <summary>
    /// Gets or sets the low price of the day.
    /// </summary>
    public double LowPrice { get; set; }
  }
}
