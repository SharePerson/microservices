using Mango.Services.ShoppingCartApi.DTO;
using Mango.Services.ShoppingCartApi.Repositories.Base;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Repositories
{
    public interface ICartRepository: IRepository<CartDto, int>
    {
        Task<bool> ClearCart(string userId);
        Task<CartDto> GetCartByUserId(string userId);
        Task<bool> RemoveFromCart(int cartDetailId);
    }
}
