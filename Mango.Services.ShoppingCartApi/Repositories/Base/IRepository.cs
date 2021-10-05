using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Repositories.Base
{
    public interface IRepository<DataType, KeyIdentifierType> where DataType: class
    {
        Task<IEnumerable<DataType>> GetAll();
        Task<DataType> Get(KeyIdentifierType key);
        Task<DataType> CreateUpdate(DataType item);
        Task<bool> Delete(KeyIdentifierType key);
    }
}
