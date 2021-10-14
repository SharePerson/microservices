using Mango.Services.OrderApi.Models;
using Mango.Services.OrderApi.Repositories.Base;
using System.Threading.Tasks;

namespace Mango.Services.OrderApi.Repositories
{
    public interface IOrderRepository: IRepository<OrderHeader, int>
    {
        Task UpdateOrderPaymentStatus(int orderHeaderId, bool isPaid);
    }
}
