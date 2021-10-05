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

            if (product == null)
            {
                await _db.Product.AddAsync(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }

            var cartHeader = await _db.CartHeader.AsNoTracking().FirstOrDefaultAsync(ch => ch.UserId == cart.CartHeader.UserId);

            if (cartHeader == null)
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

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeader.FirstOrDefaultAsync(ch => ch.UserId == userId);

            if (cartHeaderFromDb == null) return false;

            _db.CartDetail.RemoveRange(_db.CartDetail.Where(cd => cd.CartHeaderId == cartHeaderFromDb.Id));
            _db.CartHeader.Remove(cartHeaderFromDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _db.CartHeader.FirstOrDefaultAsync(ch => ch.UserId == userId)
            };

            cart.CartDetails = await _db.CartDetail.Where(cd => cd.CartHeaderId == cart.CartHeader.Id)
                .Include(cd => cd.Product)
                .ToListAsync();

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailId)
        {
            CartDetail cartDetail = await _db.CartDetail.FirstOrDefaultAsync(cd => cd.Id == cartDetailId);
            if (cartDetail == null) return false;

            int totalCartItems = await _db.CartDetail.Where(cd => cd.CartHeaderId == cartDetail.CartHeaderId).CountAsync();

            _db.CartDetail.Remove(cartDetail);

            if(totalCartItems == 1)
            {
                CartHeader cartHeader = await _db.CartHeader.FirstOrDefaultAsync(ch => ch.Id == cartDetail.CartHeaderId);
                _db.CartHeader.Remove(cartHeader);
            }

            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> Get(int key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartDto>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
