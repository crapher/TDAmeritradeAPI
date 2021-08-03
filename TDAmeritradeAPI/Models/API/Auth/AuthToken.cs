using System;

namespace TDAmeritradeAPI.Models.API.Auth
{
    public class AuthToken
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public long expires_in { get; set; }
        public long refresh_token_expires_in { get; set; }
        public string token_type { get; set; }

        // Added for refresh token handling
        public DateTime refresh_token_expiration_date { get; set; }
    }
}
