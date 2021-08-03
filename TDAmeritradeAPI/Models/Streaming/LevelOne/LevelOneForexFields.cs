namespace TDAmeritradeAPI.Models.Streaming.LevelOne
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640608
    public enum LevelOneForexFields
    {
        Symbol = 0, // Always Required
        BidPrice = 1,
        AskPrice = 2,
        LastPrice = 3,
        BidSize = 4,
        AskSize = 5,
        TotalVolume = 6,
        LastSize = 7,
        QuoteTime = 8,
        TradeTime = 9,
        HighPrice = 10,
        LowPrice = 11,
        ClosePrice = 12,
        ExchangeId = 13,
        Description = 14,
        OpenPrice = 15,
        NetChange = 16,
        PercentageChange = 17,
        ExhangeName = 18,
        Digits = 19,
        SecurityStatus = 20,
        Tick = 21,
        TickAmount = 22,
        Product = 23,
        TradingHours = 24,
        IsTradable = 25,
        MarketMaker = 26,
        _52WkHigh = 27,
        _52WkLow = 28,
        Mark = 29
    }
}
