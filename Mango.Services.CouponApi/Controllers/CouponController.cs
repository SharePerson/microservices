using Mango.Services.CouponApi.DTO;
using Mango.Services.CouponApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<ResponseDto<CouponDto>> Get(string code)
        {
            ResponseDto<CouponDto> response = new();

            try
            {
                response.Result = await _couponRepository.Get(code);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }
    }
}
