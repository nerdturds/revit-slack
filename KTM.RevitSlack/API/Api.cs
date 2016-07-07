using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace KTM.RevitSlack.API
{
    public class Api
    {
        private bool _useProxy = false;
        public RestClient _restClient { get; set; }
        public RestRequest _restRequest { get; set; }

        public string _restPath { get; set; }
        public Method _method { get; set; }
        public DataFormat _dataFormat { get; set; }
        private static string _token { get; set; }

        static readonly string _executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// API handler
        /// </summary>
        /// <param name="restPath">method path</param>
        /// <param name="method">request method</param>
        /// <param name="mheader">domain header</param>
        public Api(string restPath, Method method, string mheader = null, bool tokenize = false)
        {
            _restPath = restPath;
            _method = method;

            Setup(mheader, tokenize);
        }

        #region Public static methods

        /// <summary>
        /// Feth a random gif based on search term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns>URL of gif</returns>
        public static Dictionary<string, object> RandomGiphyUrl(string searchTerm)
        {
            var client = new RestClient("https://api.giphy.com/");
            var request = new RestRequest(String.Format("v1/gifs/random?api_key=dc6zaTOxFJmzC&tag={0}", searchTerm),
              Method.GET);

            try
            {
                var response = client.Execute(request);
                var ds = new JsonDeserializer();
                var dict = ds.Deserialize<Dictionary<string, object>>(response);
                var data = dict["data"] as Dictionary<string, object>;
                return data;

            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Setup the API object
        /// </summary>
        private void Setup(string header = "http://localhost/", bool tokenize = false)
        {
            _restClient = new RestClient(header);
            _restRequest = new RestRequest(_restPath, _method);
            //tokenize
            if (tokenize)
            {
                if (_token.Length < 1) _token = GetOAuthToken();
                _restRequest.AddHeader("Accept", "application/json");
                _restRequest.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            }
            _restRequest.RequestFormat = _dataFormat;
        }

        /// <summary>
        /// requests the oauth token
        /// </summary>
        /// <returns></returns>
        private static string GetOAuthToken()
        {
            string url = "https://localhost.com/";
            string client_id = "CLIENTID";
            string client_secret = "CLIENTSECRET";

            //request token
            var restclient = new RestClient(url);
            RestRequest request = new RestRequest("oauth") { Method = Method.POST };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", client_secret);
            request.AddParameter("grant_type", "client_credentials");


            var tResponse = restclient.Execute(request);
            var responseJson = tResponse.Content;
            var token = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson)["access_token"].ToString();

            return token.Length > 0 ? token : null;
        }

        #region Private Methods

        #endregion
    }
}