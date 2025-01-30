using ColetorA41.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        protected readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        public BaseService()
        {
            _httpClient = new HttpClient(DependencyService.Get<IHTTPClientHandlerCreationService>().GetInsecureHandler());
            _httpClient.BaseAddress = new Uri(Ambiente.PrefixoUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Ambiente.UsuarioSenhaBase64);
            _httpClient.DefaultRequestHeaders.Add("x-totvs-server-alias", Ambiente.AliasAppServer);
            _httpClient.DefaultRequestHeaders.Add("CompanyId", Ambiente.EmpresaSelecionada.CodEmpresa);
            _httpClient.Timeout = Timeout.InfiniteTimeSpan;
            _httpClient.DefaultRequestHeaders.Connection.Add("Keep-Alive");
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
                var response = await _httpClient.GetAsync(endpoint + stringParam.ToString());

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                response.Dispose();
             
                return System.Text.Json.JsonSerializer.Deserialize<T>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Impossível obter dados: {ex.Message}");
                //await Shell.Current.DisplayAlert("Erro!", ex.Message, "OK");
                throw new Exception(ex.Message);
                // return default;
            }
            finally
            {
                
                
            }

        }


        protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string metodo, TRequest requestBody = default)
        {
            //Montar Request
            var request = new HttpRequestMessage { Method = HttpMethod.Post, RequestUri = new Uri(Path.Combine(Ambiente.PrefixoUrl, metodo)) };
            if (requestBody != null)
            {
                var json = JsonSerializer.Serialize(requestBody);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            try
            {
                //Executar
                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Erro: {response.StatusCode}");
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<TResponse>(responseJson);
                return responseData;
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Impossível obter dados: {ex.Message}");
                // await Shell.Current.DisplayAlert("Erro!", ex.Message, "OK");
                throw new Exception(ex.Message);

                // return default;
            }
            finally { 
               request.Dispose();
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

