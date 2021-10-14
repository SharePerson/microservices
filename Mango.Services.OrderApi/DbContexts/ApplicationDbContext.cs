using Microsoft.EntityFrameworkCore;

namespace Mango.Services.OrderApi.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
