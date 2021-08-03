using RestSharp;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TDAmeritradeAPI.Models.API.Auth;
using Utf8Json;
using Utf8Json.Resolvers;

namespace TDAmeritradeAPI.Client
{
    internal static class TDAuth
    {
        private const string kAuthUrl = "https://api.tdameritrade.com/v1/oauth2/token";

        public async static Task<AuthToken> GetAuthTokenFromFile(string tokensFile, string clientId, bool refreshIfNeeded = false)
        {
            if (string.IsNullOrWhiteSpace(tokensFile))
                throw new ArgumentNullException("tokensFile is NULL");

            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException("clientId is NULL or empty");

            string authTokensStr = File.ReadAllText(tokensFile);
            var authToken = JsonSerializer.Deserialize<AuthToken>(authTokensStr);

            authToken = await UpdateRefreshToken(tokensFile, authToken, clientId, refreshIfNeeded);
            return authToken;
        }

        public async static Task<AuthToken> UpdateAccessToken(string tokensFile, AuthToken authToken, string clientId)
        {
            if (null == authToken)
                throw new ArgumentNullException("authToken is NULL");

            var token = await GetAccessToken(clientId, authToken.refresh_token);

            using var fs = new FileStream(tokensFile, FileMode.Open, FileAccess.ReadWrite);
            authToken.access_token = token.access_token;
            authToken.expires_in = token.expires_in;

            using var ws = new StreamWriter(fs);
            var authTokenRaw = JsonSerializer.Serialize<AuthToken>(authToken, StandardResolver.ExcludeNull);
            var authTokenString = Encoding.UTF8.GetString(authTokenRaw);
            ws.Write(authTokenString);

            return authToken;
        }

        private async static Task<AuthToken> UpdateRefreshToken(string tokensFile, AuthToken authToken, string clientId, bool refreshIfNeeded)
        {
            if (!refreshIfNeeded || null == authToken)
                return authToken;

            if (null == authToken.refresh_token)
                throw new ArgumentNullException("RefreshToken is NULL");

            if (null != authToken.refresh_token_expiration_date && DateTime.MinValue != authToken.refresh_token_expiration_date &&
                authToken.refresh_token_expiration_date.AddDays(-30) > DateTime.UtcNow.Date)
                return authToken;

            using var fs = new FileStream(tokensFile, FileMode.Open, FileAccess.ReadWrite);
            authToken = await GetRefreshToken(clientId, authToken.refresh_token);
            authToken.refresh_token_expiration_date = DateTime.UtcNow.AddSeconds(authToken.refresh_token_expires_in);

            using var ws = new StreamWriter(fs);
            var authTokenRaw = JsonSerializer.Serialize<AuthToken>(authToken, StandardResolver.ExcludeNull);
            var authTokenString = Encoding.UTF8.GetString(authTokenRaw);
            ws.Write(authTokenString);

            return authToken;
        }

        private async static Task<AuthToken> GetRefreshToken(string clientId, string refreshToken)
        {
            var client = new RestClient(kAuthUrl);
            client.Proxy = TDClient.Proxy;

            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", refreshToken);
            request.AddParameter("access_type", "offline");
            request.AddParameter("code", string.Empty);
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", string.Empty);

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new Exception(response.StatusDescription);

            return JsonSerializer.Deserialize<AuthToken>(response.Content);
        }

        private async static Task<AuthToken> GetAccessToken(string clientId, string refreshToken)
        {
            var client = new RestClient(kAuthUrl);
            client.Proxy = TDClient.Proxy;

            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", refreshToken);
            request.AddParameter("access_type", string.Empty);
            request.AddParameter("code", string.Empty);
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", string.Empty);

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new Exception(response.StatusDescription);

            return JsonSerializer.Deserialize<AuthToken>(response.Content);
        }
    }
}
