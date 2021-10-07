using Mango.Web.Models;
using Mango.Web.Models.Base;
using Mango.Web.Services.Base;
using Mango.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mango.Web.Services
{
    public class CouponService : ServiceBase<CouponDto>, ICouponService
    {
        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public string ApiBaseUrl => ApplicationSettings.CouponApiBase;

        public async Task<ResponseDto<CouponDto>> GetAsync(string key, string token = null)
        {
            return await SendAsync<ResponseDto<CouponDto>>(new ApiRequest<CouponDto>
            {
                ApiType = ApplicationSettings.ApiType.GET,
                Url = ApiBaseUrl + "/api/coupons/" + key,
                AccessToken = token
            });
        }

        public Task<ResponseDto<CouponDto>> CreateAsync(CouponDto item, string token = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseDto<bool>> DeleteAsync(string key, string token = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseDto<IEnumerable<CouponDto>>> GetAllAsync(string token = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseDto<CouponDto>> UpdateAsync(CouponDto item, string token = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
