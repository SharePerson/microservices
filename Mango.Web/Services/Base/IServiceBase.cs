using Mango.Web.Models;
using Mango.Web.Models.Base;
using System;
using System.Threading.Tasks;

namespace Mango.Web.Services.Base
{
    public interface IServiceBase<DataType>: IDisposable where DataType: class
    {
        ResponseDto<DataType> Response { set; get; }
        Task<T> SendAsync<T>(ApiRequest<DataType> request);
    }
}
