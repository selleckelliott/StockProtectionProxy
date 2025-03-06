using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DotNetEnv;

namespace StockProtectionProxy
{
  public class Program
  {
    //List counts how many times a user enters "premium."
    private static List<int> premium = new List<int>();
    public static async Task Main()
    {
      Console.WriteLine("Welcome to our stock lookup!");
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine("To sample our paid experience, type the word: premium.");
      Console.WriteLine("Or you can access yesterday's close price by typing the word: free");
      Console.WriteLine();
      Console.WriteLine("Please note: you can sample the paid experience only 3 times.");
      Console.WriteLine();
      string inputUserType = Console.ReadLine().ToLower();

      //Loop requires user to enter a valid input of either "free" or "premium."
      while (inputUserType != null)
      {
        if (inputUserType != "free" && inputUserType != "premium")
        {
          Console.Clear();
          Console.WriteLine("Error: Please enter a valid input");
          inputUserType = Console.ReadLine().ToLower();
          Console.Clear();
        }
        else if (inputUserType == "free")
        {
          break;
        }
        else if (inputUserType == "premium")
        {
          break;
        }
      }

      //Counts how many times "premium" has been entered.
      //If it exceeds the allowed amount, it automatically changes the variable to the "free" type.
      if (premium.Count > 4)
      {
        Console.WriteLine("Sorry, you have reached your limit of complimentary full stock quotes.");
        Console.WriteLine("Proceeding with free market close quote...");
        inputUserType = "free";
      }

      Console.WriteLine("Loading quote screen...");
      Thread.Sleep(3000);
      Console.Clear();

      //Calls method to lookup stock quote, either full or just for yesterday.
      await StockLookUp(inputUserType);

      //Loop requires user to decide whether they would like to continue. Allows for continuous quotes,
      //but still limits them to the 3 "premium" quotes.
      while (inputUserType != null)
      {
        Console.WriteLine();
        Console.WriteLine("Would you like another quote? Press 'y' for yes or 'n' for no:");
        string newQuote = Console.ReadLine().ToLower();
        Console.Clear();

        if (newQuote != "y" && newQuote != "n")
        {
          Console.WriteLine("Error: Please enter a valid input");
          newQuote = Console.ReadLine().ToLower();
          Console.Clear();
        }
        else if (newQuote == "n")
        {
          Console.Clear();
          Console.WriteLine("Thank you for using our service. Press any key to exit.");
          break;
        }
        else if (newQuote == "y")
        {
          Console.Clear();
          await StockLookUp(inputUserType);
        }
        else
        {
          break;
        }
      }
    }

    /// <summary>
    /// Checks if user has exceeded "premium" limit. Proceeds with looking up stock quote.
    /// </summary>
    /// <param name="inputUserType"></param>
    private static async Task StockLookUp(string inputUserType)
    {
      Console.WriteLine($"DEBUG: Premim Count = {premium.Count}");
      if (premium.Count >= 3)
      {
        Console.WriteLine("Sorry, you have reached your limit of complimentary full stock quotes.");
        Console.WriteLine("Proceeding with free market close quote.");
        inputUserType = "free";
      }
      Console.WriteLine("Please enter a stock symbol in the S&P 500:");
      string inputStock = Console.ReadLine().ToUpper();
      Console.Clear();

      StockProxyService getStock = new StockProxyService();
      StockResponse stockData = await getStock.GetStockPrice(inputStock, inputUserType);

      if (stockData != null)
      {
        Console.WriteLine($"Stock Symbol: {stockData.Symbol}");
        Console.WriteLine($"Company Name: {stockData.Name}");
        Console.WriteLine($"Market Close Price: {stockData.MarketClosePrice}");

        if (inputUserType == "premium")
        {
          Console.WriteLine($"Current Price: {stockData.CurrentPrice}");
          Console.WriteLine($"Open Price: {stockData.OpenPrice}");
          Console.WriteLine($"High Price: {stockData.HighPrice}");
          Console.WriteLine($"Low Price: {stockData.LowPrice}");

          premium.Add(1);
        }
      }
      else
      {
        Console.WriteLine("Error: Failed to retrieve stock data.");
      }
    }
  }
}
