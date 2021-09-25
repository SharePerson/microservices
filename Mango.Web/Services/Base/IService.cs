using Mango.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Web.Services.Base
{
    public interface IService<DataType, KeyType>: IServiceBase<DataType> where DataType: class
    {
        Task<ResponseDto<IEnumerable<DataType>>> GetAllAsync();
        Task<ResponseDto<DataType>> GetAsync(KeyType key);
        Task<ResponseDto<DataType>> CreateAsync(DataType item);
        Task<ResponseDto<DataType>> UpdateAsync(DataType item);
        Task<ResponseDto<bool>> DeleteAsync(KeyType key);
    }
}
