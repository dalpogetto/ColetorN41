﻿using ColetorA41.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ColetorA41.Services
{
    public class BaseService
    {
        /// <summary>
        /// An instance of <see cref="HttpClient"/>.
        /// </summary>
        protected readonly IHttpClientFactory _httpClientFactory;
        public HttpClient _httpClient;
        private readonly IConfiguration _config;

        

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        public BaseService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {

            
            _config = config;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("coletor");
            _httpClient.BaseAddress = new Uri(_config["BASE_URL"]);
            _httpClient.DefaultRequestHeaders.Add("x-totvs-server-alias", _config["ALIAS_APPSERVER"]);
            _httpClient.DefaultRequestHeaders.Add("CompanyId", _config["EMPRESA_PADRAO"]);
          //  _httpClient = new HttpClient(DependencyService.Get<IHTTPClientHandlerCreationService>().GetInsecureHandler());
            
        }

        protected async Task<T?> GetAsync<T>(string endpoint, NameValueCollection parameters = null)
        {
            var stringParam = new StringBuilder();

            if (!IsInternetAvailable())
            {
                return default;
            }

            if (parameters != null)
            {
                string str = "?";
                for (int index = 0; index < parameters.Count; ++index)
                {
                    stringParam.Append(str + parameters.AllKeys[index] + "=" + parameters[index]);
                    str = "&";
                }
            }

            try
            {

                //Montar httpclient
               
                var response = await _httpClient.GetAsync(endpoint + stringParam.ToString());
                var responseStream = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(responseStream);
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Impossível obter dados: {ex.Message}");
                throw new Exception(ex.Message);
            }
            finally
            {
                
                
            }

        }

        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string metodo, TRequest requestBody = default)
        {

            //Montar httpclient
      
            /*Montar Request*/
            var request = new HttpRequestMessage { Method = HttpMethod.Post, RequestUri = new Uri(Path.Combine(_config["BASE_URL"], metodo)) };
            if (requestBody != null)
            {
               // var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
                var json = JsonConvert.SerializeObject(requestBody);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            try
            {
                var response = await _httpClient.SendAsync(request);
                var responseStream = await response.Content.ReadAsStringAsync();
                {
                    var data = JsonConvert.DeserializeObject<TResponse>(responseStream);
                    return data;
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Impossível obter dados: {ex.Message}");
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }



        protected async Task<HttpResponseMessage?> DeleteAsync(string endpoint)
        {
            if (!IsInternetAvailable())
            {
                return null;
            }

            try
            {
                //var _httpClient = _httpClientFactory.CreateClient("coletor");
                var _httpClient = new HttpClient(DependencyService.Get<IHTTPClientHandlerCreationService>().GetInsecureHandler());
                return await _httpClient.DeleteAsync(endpoint);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Impossível eliminar registro: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro!", "Impossível eliminar registro.", "OK");
                return null;
            }
            
        }

        /// <summary>
        /// Checks if an internet connection is available.
        /// </summary>
        /// <returns><c>true</c> if an internet connection is available; otherwise, <c>false</c>.</returns>
        private bool IsInternetAvailable()
        {
            NetworkAccess accessType = Connectivity.NetworkAccess;

            if (accessType != NetworkAccess.Internet)
            {
                if (Shell.Current != null)
                {
                    if (accessType == NetworkAccess.ConstrainedInternet)
                    {
                        Shell.Current.DisplayAlert("Error!", "Internet access is limited.", "OK");
                    }
                    else
                    {
                        Shell.Current.DisplayAlert("Error!", "No internet access.", "OK");
                    }
                }

                return false;
            }

            return true;
        }
    }
}

