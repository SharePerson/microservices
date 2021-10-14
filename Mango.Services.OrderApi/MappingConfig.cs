using AutoMapper;

namespace Mango.Services.OrderApi
{ 
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                //config.CreateMap<ProductDto, Product>().ReverseMap();
                
            });
        }
    }
}
