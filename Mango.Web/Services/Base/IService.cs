using Mango.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Web.Services.Base
{
    public interface IService<DataType, KeyType>: IServiceBase<DataType> where DataType: class
    {
        Task<ResponseDto<IEnumerable<DataType>>> GetAllAsync(string token = null);
        Task<ResponseDto<DataType>> GetAsync(KeyType key, string token = null);
        Task<ResponseDto<DataType>> CreateAsync(DataType item, string token = null);
        Task<ResponseDto<DataType>> UpdateAsync(DataType item, string token = null);
        Task<ResponseDto<bool>> DeleteAsync(KeyType key, string token = null);
    }
}
