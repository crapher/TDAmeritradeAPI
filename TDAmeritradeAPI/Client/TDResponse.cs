namespace TDAmeritradeAPI.Client
{
    public class TDResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
