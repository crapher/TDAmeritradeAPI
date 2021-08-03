namespace TDAmeritradeAPI.Models.Streaming.Chart
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640592
    public enum ChartOptionsFields
    {
        Symbol = 0, // Always Required
        ChartTime = 1,
        OpenPrice = 2,
        HighPrice = 3,
        LowPrice = 4,
        ClosePrice = 5,
        Volume = 6
    }
}
