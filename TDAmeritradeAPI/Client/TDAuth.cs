using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using TDAmeritradeAPI.Models.Auth;

namespace TDAmeritradeAPI.Client
{
    public static class TDAuth
    {
        private const string AuthUrl = "https://api.tdameritrade.com/v1/oauth2/token";
        /// <summary>
        /// Get code to get access token. This is for console app.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="redirectUri"></param>
        public static void GetAuthCode(string clientId, string redirectUri)
        {
            Process.Start(ChromeAppFileName, $"https://auth.tdameritrade.com/oauth?client_id={clientId}@AMER.OAUTHAP&response_type=code&redirect_uri={redirectUri}");
        }
        /// <summary>
        /// Get refresh and access token from Auth Code
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="code"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public static AuthToken GetAccessToken(string clientId, string code, string redirectUri)
        {
            var client = new RestClient(AuthUrl);
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", clientId);
            request.AddParameter("access_type", "offline");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", redirectUri);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<AuthToken>(response.Content);
        }
        /// <summary>
        /// Update refresh token with refresh token (Once every 90 days)
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static AuthToken GetRefreshToken(string clientId, string refreshToken)
        {
            var client = new RestClient(AuthUrl);
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", refreshToken);
            request.AddParameter("access_type", "offline");
            request.AddParameter("code", "");
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", "");
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<AuthToken>(response.Content);
        }
        /// <summary>
        /// Update access token with refresh token (Once every 30 minutes)
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static AuthToken GetAccessToken(string clientId, string refreshToken)
        {
            var client = new RestClient(AuthUrl);
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", refreshToken);
            request.AddParameter("access_type", "");
            request.AddParameter("code", "");
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", "");
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<AuthToken>(response.Content);
        }
        /// <summary>
        /// Gets location of Chrome.exe
        /// </summary>
        private const string ChromeAppKey = @"\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";
        private static string ChromeAppFileName =>
            (string)(Registry.GetValue("HKEY_LOCAL_MACHINE" + ChromeAppKey, "", null) ??
                     Registry.GetValue("HKEY_CURRENT_USER" + ChromeAppKey, "", null));
    }
}
