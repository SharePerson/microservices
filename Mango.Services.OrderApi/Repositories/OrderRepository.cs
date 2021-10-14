using Mango.Services.OrderApi.DbContexts;
using Mango.Services.OrderApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.OrderApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContext;

        public OrderRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderHeader> CreateUpdate(OrderHeader item)
        {
            await using var _db = new ApplicationDbContext(_dbContext);
            _db.OrderHeader.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool isPaid)
        {
            await using var _db = new ApplicationDbContext(_dbContext);
            var orderHeader = await _db.OrderHeader.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
            if(orderHeader != null)
            {
                orderHeader.PaymentStatus = isPaid;
                _db.OrderHeader.Update(orderHeader);
                await _db.SaveChangesAsync();
            }
        }



        #region NotImplemented
        public Task<bool> Delete(int key)
        {
            throw new System.NotImplementedException();
        }

        public Task<OrderHeader> Get(int key)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<OrderHeader>> GetAll()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
