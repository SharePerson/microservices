using AutoMapper;
using Mango.Services.ShoppingCartApi.DTO;
using Mango.Services.ShoppingCartApi.Models;

namespace Mango.Services.ShoppingCartApi
{ 
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<Cart, CartDto>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetail, CartDetailDto>().ReverseMap();
            });
        }
    }
}
