using Mango.Services.ShoppingCartApi.DTO;
using Mango.Services.ShoppingCartApi.Repositories.Base;

namespace Mango.Services.ShoppingCartApi.Repositories
{
    public interface ICartRepository: IRepository<CartDto, int>
    {

    }
}
