using Mango.Services.ShoppingCartApi.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client;

        public CouponRepository(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _client.BaseAddress = new Uri(configuration["ServiceUrls:CouponApi"]);
        }

        public async Task<CouponDto> Get(string key)
        {
            var response = await _client.GetAsync($"api/coupons/{key}");
            var responsePayload = await response.Content.ReadAsStringAsync();
            ResponseDto<CouponDto> responseDto = JsonConvert.DeserializeObject<ResponseDto<CouponDto>>(responsePayload);
            if(responseDto != null && responseDto.IsSuccess)
            {
                return responseDto.Result;
            }

            return null;
        }



        public Task<CouponDto> CreateUpdate(CouponDto item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CouponDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
