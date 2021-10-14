using Mango.MessageBus;
using Mango.Services.ShoppingCartApi.DTO;
using Mango.Services.ShoppingCartApi.Messages;
using Mango.Services.ShoppingCartApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMessageBus _messageBus;
        private readonly ICouponRepository _couponRepository;

        public CartController(ICartRepository cartRepository, IMessageBus messageBus, ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository;
            _messageBus = messageBus;
            _couponRepository = couponRepository;
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

        [HttpPost("{userId}/coupons/{code}")]
        public async Task<ResponseDto<bool>> ApplyCoupon(string userId, string code)
        {
            ResponseDto<bool> response = new();

            try
            {
                bool success = await _cartRepository.ApplyCoupon(userId, code);
                response.Result = success;
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

        [HttpDelete("{userId}/coupons")]
        public async Task<ResponseDto<bool>> RemoveCoupon(string userId)
        {
            ResponseDto<bool> response = new();

            try
            {
                bool success = await _cartRepository.RemoveCoupon(userId);
                response.Result = success;
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

        [HttpPost("checkouts")]
        public async Task<object> Checkout(CartDto cartDto)
        {
            ResponseDto<bool> response = new();

            try
            {
                CartDto cartFromDb = await _cartRepository.GetCartByUserId(cartDto.CartHeader.UserId);

                if (cartFromDb == null) return BadRequest();

                //coupon validation
                if(!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    CouponDto activeCoupon = await _couponRepository.Get(cartDto.CartHeader.CouponCode);

                    if (activeCoupon == null) return BadRequest();

                    double totalPriceWithoutDiscount = cartDto.CartHeader.DiscountTotal + cartDto.CartHeader.OrderTotal;
                    double discountPercentage = 100 - (cartDto.CartHeader.OrderTotal / totalPriceWithoutDiscount * 100);
                    string discountString = discountPercentage.ToString();

                    if (discountString != activeCoupon.DiscountAmount)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessages = new List<string> { "Coupon changed, please confirm" };
                        response.DisplayMessage = "Coupon changed, please confirm";
                        return response;
                    }
                }

                CheckoutMessage checkoutMessage = new()
                {
                    CardNumber = cartDto.Checkout.CardNumber,
                    CartDetails = cartFromDb.CartDetails.ToList(),
                    CartHeaderId = cartDto.CartHeader.Id,
                    CouponCode = cartDto.CartHeader.CouponCode,
                    CVV = cartDto.Checkout.CVV,
                    DiscountTotal = cartDto.CartHeader.DiscountTotal,
                    Email = cartDto.Checkout.Email,
                    FirstName = cartDto.Checkout.FirstName,
                    LastName = cartDto.Checkout.LastName,
                    MMYY = cartDto.Checkout.MMYY,
                    OrderTotal = cartDto.CartHeader.OrderTotal,
                    Phone = cartDto.Checkout.Phone,
                    PickupTime = cartDto.Checkout.PickupTime,
                    UserId = cartDto.CartHeader.UserId,
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now
                };

                await _messageBus.PublishMessage(checkoutMessage, "checkoutmessagetopic");

                //add message to process order
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
