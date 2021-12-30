using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RmbRazorPages.Library.ApiClient
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }
        public ApiHelper()
        {
            InitializeClient();
        }
        private void InitializeClient()
        {
            string api = "https://rmbcloneapi.azurewebsites.net/";
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
