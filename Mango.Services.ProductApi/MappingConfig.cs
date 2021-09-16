using AutoMapper;
using Mango.Services.ProductApi.DTO;
using Mango.Services.ProductApi.Models;

namespace Mango.Services.ProductApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });
        }
    }
}
