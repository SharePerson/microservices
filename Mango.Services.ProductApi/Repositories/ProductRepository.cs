using Mango.Services.ProductApi.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<ProductDto> CreateUpdate(ProductDto item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(ProductDto item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductDto> Get(int keyType)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
