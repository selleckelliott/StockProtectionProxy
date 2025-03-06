using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockProtectionProxy
{
  public interface IStockService
  {
    Task<StockResponse> GetStockPrice(string symbol, string userType);
  }
}
