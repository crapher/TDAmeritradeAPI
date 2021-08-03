using System;
using System.Net;
using TDAmeritradeAPI.Client;

namespace TDAmeritradeAPI.ExampleStreaming
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Client */
			string fileName = "{FILENAME}";
            string clientId = "{CONSUMER_KEY}";
            TDClient client = new TDClient(fileName, clientId);

            /* Streamer */
            var streamer = new TDStreamClient(client);

            streamer.OnSubscribe += (sender, s) => { Console.WriteLine(s); };

            streamer.OnLogin += (sender, data) =>
            {
                if (data.Code == 0)
                {
                    //streamer.OnAccountActivity += (sender, account) =>
                    //{
                    //    if (null != account && account.Length > 0)
                    //        Console.WriteLine($"{account[0].Account}\n{account[0].MessageType}\n{account[0].MessageData}");
                    //    else
                    //        Console.WriteLine("No Account Available");
                    //};
                    //streamer.SubscribeAccountActivity();

                    //streamer.OnLevelOneQuotes += (sender, quotes) =>
                    //{
                    //    foreach (var quote in quotes)
                    //        Console.WriteLine($"{quote.Symbol} - Is Delayed: {quote.Delayed} - Last: {quote.LastPrice}");
                    //};
                    //streamer.SubscribeLevelOneQuotes(new[] { "AAPL", "MSFT" });

                    //streamer.OnLevelOneOptions += (sender, quotes) =>
                    //{
                    //    foreach (var quote in quotes)
                    //        Console.WriteLine($"{quote.Symbol} - " +
                    //            $"Is Delayed: {quote.Delayed} - Last: {quote.LastPrice}");
                    //};
                    //streamer.SubscribeLevelOneOptions(new[] { "TSLA_012023C1625", "MSFT_061623C310" });

                    //streamer.OnLevelOneFutures += (sender, quotes) =>
                    //{
                    //    foreach (var quote in quotes)
                    //        Console.WriteLine($"{quote.Symbol} - Is Delayed: {quote.Delayed} - Last: {quote.LastPrice} - AskSize: {quote.AskSize} - BidSize: {quote.BidSize}");
                    //};
                    //streamer.SubscribeLevelOneFutures(new[] { "/ES", "/CL" });

                    //streamer.OnLevelOneFuturesOptions += (sender, quotes) =>
                    //{
                    //    foreach (var quote in quotes)
                    //        Console.WriteLine($"{quote.Symbol} - Is Delayed: {quote.Delayed} - Last: {quote.LastPrice} - AskSize: {quote.AskSize} - BidSize: {quote.BidSize}");
                    //};
                    //streamer.SubscribeLevelOneFuturesOptions(new[] { "./GEZ22C92", "./GEZ22C95" });

                    //streamer.OnLevelOneForex += (sender, quotes) =>
                    //{
                    //    foreach (var quote in quotes)
                    //        Console.WriteLine($"{quote.Symbol} - Is Delayed: {quote.Delayed} - Last: {quote.LastPrice} - AskSize: {quote.AskSize} - BidSize: {quote.BidSize}");
                    //};
                    //streamer.SubscribeLevelOneForex(new[] { "EUR/USD", "USD/CAD" });

                    //streamer.OnChartEquity += (sender, quotes) =>
                    //{
                    //    foreach (var quote in quotes)
                    //        Console.WriteLine($"{quote.Symbol} - O: {quote.OpenPrice} - H: {quote.HighPrice} - L: {quote.LowPrice} - C: {quote.ClosePrice} - V: {quote.Volume} - T: {DateTimeOffset.FromUnixTimeSeconds(quote.ChartTime / 1000).ToLocalTime()}");
                    //};
                    //streamer.SubscribeChartEquity(new[] { "AAPL" });

                    //streamer.OnChartFutures += (sender, quotes) =>
                    //{
                    //    foreach (var quote in quotes)
                    //        Console.WriteLine($"{quote.Symbol} - O: {quote.OpenPrice} - H: {quote.HighPrice} - L: {quote.LowPrice} - C: {quote.ClosePrice} - V: {quote.Volume} - T: {DateTimeOffset.FromUnixTimeSeconds(quote.ChartTime / 1000).ToLocalTime()}");
                    //};
                    //streamer.SubscribeChartFutures(new[] { "/ES" });

                    //streamer.OnChartOptions += (sender, quotes) =>
                    //{
                    //    foreach (var quote in quotes)
                    //        Console.WriteLine($"{quote.Symbol} - O: {quote.OpenPrice} - H: {quote.HighPrice} - L: {quote.LowPrice} - C: {quote.ClosePrice} - V: {quote.Volume}");
                    //};
                    //streamer.SubscribeChartOptions(new[] { "./GEZ22C92", "./GEZ22C95" });

                    //streamer.OnListedBook += (sender, books) =>
                    //{
                    //    foreach (var book in books)
                    //    {
                    //        Console.Write($"{book.Symbol}");

                    //        if (book.Bids.Count > 0)
                    //            Console.Write($" - Bids ({book.Bids[0].Price} | {book.Bids[0].Size})");
                    //        else
                    //            Console.Write($" - Bids (null)");
                    //        if (book.Asks.Count > 0)
                    //            Console.Write($" - Asks ({book.Asks[0].Price} | {book.Asks[0].Size})");
                    //        else
                    //            Console.Write($" - Asks (null)");

                    //        Console.WriteLine();
                    //    }
                    //};
                    //streamer.SubscribeListedBook(new[] { "GE", "X" });

                    //streamer.OnNasdaqBook += (sender, books) =>
                    //{
                    //    foreach (var book in books)
                    //    {
                    //        Console.Write($"{book.Symbol}");

                    //        if (book.Bids.Count > 0)
                    //            Console.Write($" - Bids ({book.Bids[0].Price} | {book.Bids[0].Size})");
                    //        else
                    //            Console.Write($" - Bids (null)");
                    //        if (book.Asks.Count > 0)
                    //            Console.Write($" - Asks ({book.Asks[0].Price} | {book.Asks[0].Size})");
                    //        else
                    //            Console.Write($" - Asks (null)");

                    //        Console.WriteLine();
                    //    }
                    //};
                    //streamer.SubscribeNasdaqBook(new[] { "AAPL", "MSFT" });

                    //streamer.OnOptionsBook += (sender, books) =>
                    //{
                    //    foreach (var book in books)
                    //    {
                    //        Console.Write($"{book.Symbol}");

                    //        if (book.Bids.Count > 0)
                    //            Console.Write($" - Bids ({book.Bids[0].Price} | {book.Bids[0].Size})");
                    //        else
                    //            Console.Write($" - Bids (null)");
                    //        if (book.Asks.Count > 0)
                    //            Console.Write($" - Asks ({book.Asks[0].Price} | {book.Asks[0].Size})");
                    //        else
                    //            Console.Write($" - Asks (null)");

                    //        Console.WriteLine();
                    //    }
                    //};
                    //streamer.SubscribeOptionsBook(new[] { "TSLA_012023C1625", "MSFT_061623C310" });

                    //streamer.OnTimeSaleEquity += (sender, tss) =>
                    //{
                    //    foreach (var ts in tss)
                    //        Console.WriteLine($"{ts.Symbol} - Price ({ts.Price} | {ts.Size})");
                    //};
                    //streamer.SubscribeTimeSaleEquity(new[] { "AAPL" });

                    //streamer.OnTimeSaleFutures += (sender, tss) =>
                    //{
                    //    foreach (var ts in tss)
                    //        Console.WriteLine($"{ts.Symbol} - Price ({ts.Price} | {ts.Size})");
                    //};
                    //streamer.SubscribeTimeSaleFutures(new[] { "/ES" });

                    //streamer.OnTimeSaleOptions += (sender, tss) =>
                    //{
                    //    foreach (var ts in tss)
                    //        Console.WriteLine($"{ts.Symbol} - Price ({ts.Price} | {ts.Size})");
                    //};
                    //streamer.SubscribeTimeSaleOptions(new[] { "TSLA_012023C1625", "MSFT_061623C310" });
                }
            };

            streamer.Start();
            streamer.Login();
            Console.ReadKey();
            streamer.Stop();
        }
    }
}
