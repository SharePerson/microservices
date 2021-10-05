using AutoMapper;
using Mango.Services.ShoppingCartApi.DbContexts;
using Mango.Services.ShoppingCartApi.DTO;
using Mango.Services.ShoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<CartRepository> _logger;

        public CartRepository(ApplicationDbContext db, IMapper mapper, ILogger<CartRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CartDto> CreateUpdate(CartDto item)
        {
            Cart cart = _mapper.Map<Cart>(item);

            Product product = await _db.Product.FirstOrDefaultAsync(p => p.Id == item.CartDetails.FirstOrDefault().ProductId);

            if(product == null)
            {
                await _db.Product.AddAsync(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }

            var cartHeader = await _db.CartHeader.AsNoTracking().FirstOrDefaultAsync(ch => ch.UserId == cart.CartHeader.UserId);

            if(cartHeader == null)
            {
                await _db.CartHeader.AddAsync(cart.CartHeader);
                await _db.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;

                cart.CartDetails.FirstOrDefault().Product = null;
                await _db.CartDetail.AddAsync(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                CartDetail cartDetail = await _db.CartDetail.AsNoTracking()
                    .FirstOrDefaultAsync(cd => cd.ProductId == cart.CartDetails.FirstOrDefault().ProductId && cd.CartHeaderId == cartHeader.Id);

                cart.CartDetails.FirstOrDefault().Product = null;

                if (cartDetail == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    await _db.CartDetail.AddAsync(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    _db.CartDetail.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);
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

        public async Task<CartDto> Get(int key)
        {
            return _mapper.Map<CartDto>(await _db.Product.FirstOrDefaultAsync(p => p.Id == key));
        }

        public async Task<IEnumerable<CartDto>> GetAll()
        {
            return _mapper.Map<List<CartDto>>(await _db.Product.ToListAsync());
        }
    }
}
