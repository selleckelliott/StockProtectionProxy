# Stock Protection Proxy

## Overview

This project demonstrates the **Proxy Pattern** by implementing a **Protection Proxy** for accessing stock market data. The system:

- Controls access to stock data based on user type (`free` vs. `premium`).
- Implements **rate limiting** to prevent excessive free user requests.
- Caches stock data to optimize API calls.
- Loads stock symbols from a **local CSV file** to avoid unnecessary API requests.

## Features

✔ **Implements the Proxy Pattern** to control stock data access.\
✔ **Uses an external API (Finnhub) to fetch real stock market prices.**\
✔ **Rate limiting**: Free users can only request data every 30 seconds.\
✔ **Caching**: Prevents redundant API calls for the same stock symbol.\
✔ **Secure API key handling** using an `.env` file.\
✔ **Supports stock symbol lookups from an S&P 500 CSV file.**

## Installation

### Prerequisites

- **.NET 8.0 or later**
- **Finnhub API Key** (Register at [finnhub.io](https://finnhub.io/))

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/YourUsername/StockProtectionProxy.git
   cd StockProtectionProxy
   ```

2. **Set up API key securely:**

   - Create a `.env` file in the project root.
   - Add your Finnhub API key:
     ```
     FINNHUB_API_KEY=your_api_key_here
     ```
   - Ensure `.env` is ignored in `.gitignore`.

3. **Install dependencies:**

   ```bash
   dotnet add package DotNetEnv
   dotnet add package Newtonsoft.Json
   ```

4. **Run the program:**

   ```bash
   dotnet run
   ```

## Usage

1. **Run the application.**
2. Select your user type:
   - **`premium`**: Access real-time stock data.
   - **`free`**: Access only the previous day's market close price.
   - `premium` is limited to **3 uses per session**.
3. Enter a stock symbol **from the S&P 500**.
4. View the stock details:
   ```
   Stock Symbol: AAPL
   Company Name: Apple Inc.
   Market Close Price: 241.84
   Current Price: 238.03
   Open Price: 241.79
   High Price: 244.0272
   Low Price: 236.112
   ```
5. Choose whether to **request another stock quote**.

## Project Structure

```
StockProtectionProxy
├── Program.cs (Handles user input and stock lookups)
├── StockProxyService.cs (Implements proxy logic)
├── RealStockService.cs (Handles actual API requests)
├── StockResponse.cs (Defines stock data structure)
├── StockData.cs (Parses API response)
├── constituents.csv (Stores stock symbols and company names)
├── .env (Stores API key, ignored in Git)
├── .gitignore (Ensures `.env` is not committed)
```

## Error Handling

- **Invalid input handling** prompts users to enter correct values.
- **API failures** are caught, and appropriate error messages are displayed.
- **If the CSV file is missing**, an error is thrown with instructions to fix it.

## Future Enhancements

🔹 Support for **more stock exchanges** beyond the S&P 500.\
🔹 **Persistent user session tracking** for premium access limits.\
🔹 **Logging to a file** instead of console output.

