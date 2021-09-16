using AutoMapper;
using Mango.Services.ProductApi.DbContexts;
using Mango.Services.ProductApi.DTO;
using Mango.Services.ProductApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext db, IMapper mapper, ILogger<ProductRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto> CreateUpdate(ProductDto item)
        {
            Product product = _mapper.Map<Product>(item);

            if(product.Id > 0)
            {
                _db.Product.Update(product);
            }
            else
            {
                await _db.Product.AddAsync(product);
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> Delete(int key)
        {
            try
            {
                Product product = await _db.Product.FirstOrDefaultAsync(p => p.Id == key);
                if (product == null) return false;

                _db.Product.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<ProductDto> Get(int key)
        {
            return _mapper.Map<ProductDto>(await _db.Product.FirstOrDefaultAsync(p => p.Id == key));
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return _mapper.Map<List<ProductDto>>(await _db.Product.ToListAsync());
        }
    }
}
