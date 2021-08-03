namespace TDAmeritradeAPI.Models.Streaming
{
    internal interface IUpdatableBySymbol<T>
    {
        string Symbol { get; set; }

        void Update(T updatedObject);
    }
}
