using Mango.Web.Models;
using Mango.Web.Models.Base;
using Mango.Web.Services.Base;
using Mango.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mango.Web.Services
{
    public class ProductService : ServiceBase<ProductDto>, IProductService
    {
        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public string ApiBaseUrl => ApplicationSettings.ProductApiBase;

        public async Task<ResponseDto<ProductDto>> CreateAsync(ProductDto item, string token = null)
        {
            return await SendAsync<ResponseDto<ProductDto>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.POST,
                Data = item,
                Url = ApiBaseUrl + "/api/products",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int key, string token = null)
        {
            return await SendAsync<ResponseDto<bool>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.DELETE,
                Url = ApiBaseUrl + "/api/products/" + key,
                AccessToken = token
            });
        }

        public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAllAsync(string token = null)
        {
            return await SendAsync<ResponseDto<IEnumerable<ProductDto>>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.GET,
                Url = ApiBaseUrl + "/api/products",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<ProductDto>> GetAsync(int key, string token = null)
        {
            return await SendAsync<ResponseDto<ProductDto>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.GET,
                Url = ApiBaseUrl + "/api/products/" + key,
                AccessToken = token
            });
        }

        public async Task<ResponseDto<ProductDto>> UpdateAsync(ProductDto item, string token = null)
        {
            return await SendAsync<ResponseDto<ProductDto>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.PUT,
                Data = item,
                Url = ApiBaseUrl + "/api/products",
                AccessToken = token
            });
        }
    }
}
