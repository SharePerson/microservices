using AutoMapper;
using Mango.Services.CouponApi.DTO;
using Mango.Services.CouponApi.Models;

namespace Mango.Services.CouponApi
{ 
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
        }
    }
}
