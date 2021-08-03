namespace TDAmeritradeAPI.Models.Streaming.Chart
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640589
    public enum ChartEquityFields
    {
        Symbol = 0, // Always Required
        OpenPrice = 1,
        HighPrice = 2,
        LowPrice = 3,
        ClosePrice = 4,
        Volume = 5,
        Sequence = 6,
        ChartTime = 7,
        ChartDay = 8
    }
}
