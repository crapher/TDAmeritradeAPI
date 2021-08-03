namespace TDAmeritradeAPI.Models.Streaming.LevelOne
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640605
    public enum LevelOneFuturesFields
    {
        Symbol = 0, // Always Required
        BidPrice = 1,
        AskPrice = 2,
        LastPrice = 3,
        BidSize = 4,
        AskSize = 5,
        AskId = 6,
        BidId = 7,
        TotalVolume = 8,
        LastSize = 9,
        QuoteTime = 10,
        TradeTime = 11,
        HighPrice = 12,
        LowPrice = 13,
        ClosePrice = 14,
        ExchangeId = 15,
        Description = 16,
        LastId = 17,
        OpenPrice = 18,
        NetChange = 19,
        FuturePercentChange = 20,
        ExchangeName = 21,
        SecurityStatus = 22,
        OpenInterest = 23,
        Mark = 24,
        Tick = 25,
        TickAmount = 26,
        Product = 27,
        FuturePriceFormat = 28,
        FutureTradingHours = 29,
        FutureIsTradable = 30,
        FutureMultiplier = 31,
        FutureIsActive = 32,
        FutureSettlementPrice = 33,
        FutureActiveSymbol = 34,
        FutureExpirationDate = 35
    }
}
