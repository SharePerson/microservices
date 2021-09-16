using Mango.Services.ProductApi.DTO;
using Mango.Services.ProductApi.Repositories.Base;

namespace Mango.Services.ProductApi.Repositories
{
    public interface IProductRepository: IRepository<ProductDto, int>
    {

    }
}
