namespace TDAmeritradeAPI.Models.Streaming.LevelOne
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640582
    public enum AccountActivityFields
    {
        SubscriptionKey = 0, // Always Required
        AccountNumber = 1,
        MessageType = 2,
        MessageData = 3
    }
}
