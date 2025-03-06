using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockProtectionProxy
{
  public class StockRequest
  {
    /// <summary>
    /// Gets or sets the stock symbol.
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the company name.
    /// </summary>
    public string Name { get; set; }
  }
}
