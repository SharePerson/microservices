using AutoMapper;
using Mango.Services.CouponApi.DbContexts;
using Mango.Services.CouponApi.DTO;
using Mango.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.CouponApi.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<CouponRepository> _logger;

        public CouponRepository(ApplicationDbContext db, IMapper mapper, ILogger<CouponRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<CouponDto> CreateUpdate(CouponDto item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CouponDto> Get(string key)
        {
            Coupon coupon = await _db.Coupon.FirstOrDefaultAsync(c => c.CouponCode.ToLower() == key.ToLower());
            return _mapper.Map<CouponDto>(coupon);
        }

        public Task<IEnumerable<CouponDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
