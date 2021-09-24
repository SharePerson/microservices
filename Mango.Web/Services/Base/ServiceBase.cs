using Mango.Web.Models;
using Mango.Web.Models.Base;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Web.Services.Base
{
    public class ServiceBase<DataType> : IServiceBase<DataType> where DataType : class
    {
        protected IHttpClientFactory _clientFactory;
        public ResponseDto<DataType> Response { get; set; }
        public IHttpClientFactory HttpClient { set; get; }

        public ServiceBase(IHttpClientFactory httpClient)
        {
            Response = new ResponseDto<DataType>();
            this.HttpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest<DataType> request)
        {
            T dto;

            try
            {
                HttpClient client = HttpClient.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);
                client.DefaultRequestHeaders.Clear();

                if(request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = null;

                message.Method = request.ApiType switch
                {
                    ApplicationSettings.ApiType.POST => HttpMethod.Post,
                    ApplicationSettings.ApiType.PUT => HttpMethod.Put,
                    ApplicationSettings.ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

                response = await client.SendAsync(message);

                string apiContent = await response.Content.ReadAsStringAsync();

                dto = JsonConvert.DeserializeObject<T>(apiContent);
            }
            catch(Exception ex)
            {
                var responseDto = new ResponseDto<object>
                {
                    DisplayMessage = "Error",
                    IsSuccess = false
                };

                responseDto.ErrorMessages.Add(ex.Message);

                var res = JsonConvert.SerializeObject(responseDto);
                var genericResponse = JsonConvert.DeserializeObject<T>(res);
                return genericResponse;
            }

            return dto;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
