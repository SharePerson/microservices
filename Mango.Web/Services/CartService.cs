using Mango.Web.Models;
using Mango.Web.Models.Base;
using Mango.Web.Services.Base;
using Mango.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mango.Web.Services
{
    public class CartService : ServiceBase<CartDto>, ICartService
    {
        public string ApiBaseUrl => ApplicationSettings.ShoppingCartApiBase;

        public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ResponseDto<CartDto>> CreateAsync(CartDto item, string token = null)
        {
            return await SendAsync<ResponseDto<CartDto>>(new ApiRequest<CartDto>
            {
                ApiType = ApplicationSettings.ApiType.POST,
                Data = item,
                Url = ApiBaseUrl + "/api/cart",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<CartDto>> GetAsync(string key, string token = null)
        {
            return await SendAsync<ResponseDto<CartDto>>(new ApiRequest<CartDto>
            {
                ApiType = ApplicationSettings.ApiType.GET,
                Url = ApiBaseUrl + "/api/cart/" + key,
                AccessToken = token
            });
        }

        public async Task<ResponseDto<bool>> RemoveFromCart(int cartId, string token = null)
        {
            return await SendAsync<ResponseDto<bool>>(new ApiRequest<CartDto>
            {
                ApiType = ApplicationSettings.ApiType.DELETE,
                Url = ApiBaseUrl + "/api/cart/" + cartId,
                AccessToken = token
            });
        }

        public async Task<ResponseDto<CartDto>> UpdateAsync(CartDto item, string token = null)
        {
            return await SendAsync<ResponseDto<CartDto>>(new ApiRequest<CartDto>
            {
                ApiType = ApplicationSettings.ApiType.PUT,
                Data = item,
                Url = ApiBaseUrl + "/api/cart",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<bool>> ApplyCoupon(string userId, string couponCode, string token = null)
        {
            return await SendAsync<ResponseDto<bool>>(new ApiRequest<CartDto>
            {
                ApiType = ApplicationSettings.ApiType.POST,
                Url = ApiBaseUrl + "/api/cart/" + userId + "/coupons/" + couponCode,
                AccessToken = token

            });
        }

        public async Task<ResponseDto<bool>> RemoveCoupon(string userId, string token = null)
        {
            return await SendAsync<ResponseDto<bool>>(new ApiRequest<CartDto>
            {
                ApiType = ApplicationSettings.ApiType.DELETE,
                Url = ApiBaseUrl + "/api/cart/" + userId + "/coupons",
                AccessToken = token

            });
        }

        public async Task<object> Checkout(CartDto cartDto, string token = null)
        {
            return await SendAsync<ResponseDto<object>>(new ApiRequest<CartDto>
            {
                ApiType = ApplicationSettings.ApiType.POST,
                Url = ApiBaseUrl + "/api/cart/checkouts",
                AccessToken = token,
                Data = cartDto
            });
        }

        public Task<ResponseDto<IEnumerable<CartDto>>> GetAllAsync(string token = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<bool>> DeleteAsync(string key, string token = null)
        {
            throw new NotImplementedException();
        }
    }
}
