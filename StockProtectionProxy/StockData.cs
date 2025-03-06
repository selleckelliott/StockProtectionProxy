using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockProtectionProxy
{
  public class StockData
  {
    [JsonProperty("c")] // Current Price
    public double CurrentPrice { get; set; }

    [JsonProperty("pc")] // Previous Close Price
    public double PreviousClosePrice { get; set; }

    [JsonProperty("o")] // Open Price
    public double OpenPrice { get; set; }

    [JsonProperty("h")] // High Price of the Day
    public double HighPrice { get; set; }

    [JsonProperty("l")] // Low Price of the Day
    public double LowPrice { get; set; }
  }
}
