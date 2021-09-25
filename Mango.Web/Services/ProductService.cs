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

        public async Task<ResponseDto<ProductDto>> CreateAsync(ProductDto item)
        {
            return await SendAsync<ResponseDto<ProductDto>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.POST,
                Data = item,
                Url = ApplicationSettings.ProductApiBase + "/api/products"
            });
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int key)
        {
            return await SendAsync<ResponseDto<bool>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.DELETE,
                Url = ApplicationSettings.ProductApiBase + "/api/products/" + key
            });
        }

        public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAllAsync()
        {
            return await SendAsync<ResponseDto<IEnumerable<ProductDto>>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.GET,
                Url = ApplicationSettings.ProductApiBase + "/api/products"
            });
        }

        public async Task<ResponseDto<ProductDto>> GetAsync(int key)
        {
            return await SendAsync<ResponseDto<ProductDto>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.GET,
                Url = ApplicationSettings.ProductApiBase + "/api/products/" + key
            });
        }

        public async Task<ResponseDto<ProductDto>> UpdateAsync(ProductDto item)
        {
            return await SendAsync<ResponseDto<ProductDto>>(new ApiRequest<ProductDto>
            {
                ApiType = ApplicationSettings.ApiType.PUT,
                Data = item,
                Url = ApplicationSettings.ProductApiBase + "/api/products"
            });
        }
    }
}
