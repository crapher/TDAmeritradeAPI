using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;

namespace TDAmeritradeAPI.Client
{
    public partial class TDClient
    {
        private RestRequest _request;
        private RestClient _client;
        private string _clientId;
        private string _refreshToken;
        public string AccessToken { get; private set; }
        public TDClient(string accessToken)
        {
            AccessToken = accessToken;
        }
        public TDClient(string clientId, string refreshToken, string accessToken = null)
        {
            _clientId = clientId;
            _refreshToken = refreshToken;
            AccessToken = !string.IsNullOrWhiteSpace(accessToken) ? accessToken : TDAuth.GetAccessToken(clientId, refreshToken).access_token;
        }
        private async Task<TDResponse<T>> ExecuteEndPoint<T>(string endPoint, Dictionary<string, object> requestParams, Method method)
        {
            IRestResponse response;
            bool tryAgain = false;

            do
            {
                _client = new RestClient(endPoint);
                _request = new RestRequest(method);
                _request.AddHeader("Authorization", $"Bearer {AccessToken}");

                if (requestParams != null)
                {
                    foreach (var p in requestParams)
                    {
                        //TODO: Review != 0
                        if (p.Value != null)
                        {
                            _request.AddParameter(p.Key, p.Value);
                        }
                    }
                }

                response = _client.Execute(_request);
                tryAgain = !tryAgain && !CheckAccessTokenValidity(response); // Retry only once
            } while (tryAgain);

            var result = JsonSerializer.Deserialize<T>(response.Content);
            var instance = new TDResponse<T>();
            instance.Data = result;
            return instance;
        }

        private async Task<TDResponse<T>> ExecuteEndPoint<T>(string endPoint, object settings, Method method)
        {
            IRestResponse response;
            bool tryAgain = false;

            do
            {
                _client = new RestClient(endPoint);
                _request = new RestRequest(method);
                _request.AddHeader("Authorization", $"Bearer {AccessToken}");
                settings = JsonSerializer.Serialize(settings, StandardResolver.ExcludeNull);
                _request.AddJsonBody(settings);
                response = _client.Execute(_request);
                tryAgain = !tryAgain && !CheckAccessTokenValidity(response); // Retry only once
            } while (tryAgain);

            var result = JsonSerializer.Deserialize<T>(response.Content);
            var instance = new TDResponse<T>();
            instance.Success = response.IsSuccessful;
            instance.Data = result;
            instance.Error = response.ErrorMessage;
            return instance;
        }
        /// <summary>
        /// Check if the response is successful or failed
        /// </summary>
        /// <param name="response"></param>
        /// <returns>false if a token refresh was required else true</returns>
        private bool CheckAccessTokenValidity(IRestResponse response)
        {
            // Without client ID or Refresh Token we cannot get a new access token
            if (string.IsNullOrWhiteSpace(_clientId) || string.IsNullOrWhiteSpace(_refreshToken))
                return true;
            // if the status is Unauthorized. Refresh the access token.
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var authToken = TDAuth.GetRefreshToken(_clientId, _refreshToken);
                AccessToken = authToken.access_token;
                return false;
            }
            // Any other StatusCode return true and should be handled by the client
            return true;
        }
    }
}
