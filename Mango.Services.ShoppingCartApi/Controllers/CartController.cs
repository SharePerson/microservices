using Mango.Services.ShoppingCartApi.DTO;
using Mango.Services.ShoppingCartApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("{userId}")]
        public async Task<ResponseDto<CartDto>> GetCart(string userId)
        {
            ResponseDto<CartDto> response = new();

            try
            {
                CartDto cartDto = await _cartRepository.GetCartByUserId(userId);
                response.Result = cartDto;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>
                {
                    ex.Message
                };
            }

            return response;
        }

        [HttpPost]
        public async Task<ResponseDto<CartDto>> AddCart(CartDto cartDto)
        {
            ResponseDto<CartDto> response = new();

            try
            {
                cartDto = await _cartRepository.CreateUpdate(cartDto);
                response.Result = cartDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>
                {
                    ex.Message
                };
            }

            return response;
        }

        [HttpPut]
        public async Task<ResponseDto<CartDto>> UpdateCart(CartDto cartDto)
        {
            ResponseDto<CartDto> response = new();

            try
            {
                cartDto = await _cartRepository.CreateUpdate(cartDto);
                response.Result = cartDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>
                {
                    ex.Message
                };
            }

            return response;
        }

        [HttpDelete("{cartId}")]
        public async Task<ResponseDto<bool>> RemoveFromCart(int cartId)
        {
            ResponseDto<bool> response = new();

            try
            {
                bool remnoved = await _cartRepository.RemoveFromCart(cartId);
                response.Result = remnoved;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>
                {
                    ex.Message
                };
            }

            return response;
        }
    }
}
