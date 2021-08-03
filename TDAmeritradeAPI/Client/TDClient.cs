using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TDAmeritradeAPI.Models.API.Auth;
using Utf8Json;
using Utf8Json.Resolvers;

namespace TDAmeritradeAPI.Client
{
    public partial class TDClient
    {
        public static WebProxy Proxy;

        private RestRequest _request;
        private RestClient _client;

        private string _tokensFile;
        private string _clientId;

        private AuthToken _authToken;

        public TDClient(string tokensFile, string clientId)
        {
            _tokensFile = tokensFile;
            _clientId = clientId;

            _authToken = TDAuth.GetAuthTokenFromFile(tokensFile, clientId, true).Result;

            if (null == _authToken)
                throw new NullReferenceException("AuthToken is NULL");
        }

        private async Task<TDResponse<T>> ExecuteEndPoint<T>(string endPoint, Dictionary<string, object> requestParams, Method method)
        {
            IRestResponse response;
            bool tokenChanged = false;

            do
            {
                _client = new RestClient(endPoint);
                _client.Proxy = Proxy;

                _request = new RestRequest(method);
                _request.AddHeader("Authorization", $"Bearer {_authToken.access_token}");

                if (requestParams != null)
                {
                    foreach (var p in requestParams)
                        if (p.Value != null)
                            _request.AddParameter(p.Key, p.Value);
                }

                response = await _client.ExecuteAsync(_request);
                tokenChanged = !tokenChanged && !await CheckAccessTokenValidity(response); // Retry only once
            } while (tokenChanged);

            var instance = new TDResponse<T>();
            instance.Success = response.IsSuccessful;
            instance.Data = instance.Success && !string.IsNullOrWhiteSpace(response.Content) ? JsonSerializer.Deserialize<T>(response.Content) : default;
            instance.Error = response.ErrorMessage;

            return instance;
        }

        private async Task<TDResponse<T>> ExecuteEndPoint<T>(string endPoint, object settings, Method method)
        {
            IRestResponse response;
            bool tokenChanged = false;

            do
            {
                _client = new RestClient(endPoint);
                _client.Proxy = Proxy;

                _request = new RestRequest(method);
                _request.AddHeader("Authorization", $"Bearer {_authToken.access_token}");

                settings = JsonSerializer.Serialize(settings, StandardResolver.ExcludeNull);
                _request.AddJsonBody(settings);

                response = await _client.ExecuteAsync(_request);
                tokenChanged = !tokenChanged && !await CheckAccessTokenValidity(response); // Retry only once
            } while (tokenChanged);

            var instance = new TDResponse<T>();
            instance.Success = response.IsSuccessful;
            instance.Data = JsonSerializer.Deserialize<T>(response.Content);
            instance.Error = response.ErrorMessage;
            return instance;
        }

        private async Task<bool> CheckAccessTokenValidity(IRestResponse response)
        {
            // if the status is Unauthorized. Refresh the access token.
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _authToken = await TDAuth.UpdateAccessToken(_tokensFile, _authToken, _clientId);
                return false;
            }

            // Any other StatusCode return true and should be handled by the client
            return true;
        }
    }
}
