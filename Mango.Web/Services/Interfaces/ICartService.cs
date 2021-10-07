using Mango.Web.Models;
using Mango.Web.Services.Base;
using System.Threading.Tasks;

namespace Mango.Web.Services.Interfaces
{
    public interface ICartService : IService<CartDto, string>
    {
        Task<ResponseDto<bool>> RemoveFromCart(int cartId, string token = null);
    }
}
