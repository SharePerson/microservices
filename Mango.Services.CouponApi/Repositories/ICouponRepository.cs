using Mango.Services.CouponApi.DTO;
using Mango.Services.CouponApi.Repositories.Base;

namespace Mango.Services.CouponApi.Repositories
{
    public interface ICouponRepository: IRepository<CouponDto, string>
    {
    }
}
