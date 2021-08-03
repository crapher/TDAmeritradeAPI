namespace TDAmeritradeAPI.Models.API.UserInfoPreferences
{
    public class SubscriptionKey
    {
        public Key[] keys { get; set; }

        public class Key
        {
            public string key { get; set; }
        }
    }
}