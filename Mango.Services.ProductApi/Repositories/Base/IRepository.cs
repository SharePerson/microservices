using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.ProductApi.Repositories.Base
{
    public interface IRepository<DataType, KeyIdentifierType> where DataType: class
    {
        Task<IEnumerable<DataType>> GetAll();
        Task<DataType> Get(KeyIdentifierType keyType);
        Task<DataType> CreateUpdate(DataType item);
        Task<bool> Delete(DataType item);
    }
}
