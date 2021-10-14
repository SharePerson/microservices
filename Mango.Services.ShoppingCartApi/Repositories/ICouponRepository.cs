using Mango.Services.ShoppingCartApi.DTO;
using Mango.Services.ShoppingCartApi.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Repositories
{
    public interface ICouponRepository: IRepository<CouponDto, string>
    {
    }
}
