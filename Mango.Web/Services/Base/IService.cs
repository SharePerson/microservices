using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Web.Services.Base
{
    public interface IService<DataType, KeyType>: IServiceBase<DataType> where DataType: class
    {
        Task<IEnumerable<DataType>> GetAllAsync();
        Task<DataType> GetAsync(KeyType key);
        Task<DataType> CreateAsync(DataType item);
        Task<DataType> UpdateAsync(DataType item);
        Task<DataType> DeleteAsync(KeyType key);
    }
}
