using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await LoadCartDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CartDto cart)
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            if(!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
            {
                await _cartService.ApplyCoupon(cart.CartHeader.UserId, cart.CartHeader.CouponCode, accessToken);
            }
            else
            {
                await _cartService.RemoveCoupon(cart.CartHeader.UserId, accessToken);
            }
            return View(await LoadCartDto());
        }

        public async Task<IActionResult> Remove(int cartDetailId)
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            ResponseDto<bool> cartResponse = await _cartService.RemoveFromCart(cartDetailId, accessToken);

            if(cartResponse != null && cartResponse.IsSuccess && cartResponse.Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(await LoadCartDto());
        }

        private async Task<CartDto> LoadCartDto()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string accessToken = await HttpContext.GetTokenAsync("access_token");

            ResponseDto<CartDto> cartResponse = await _cartService.GetAsync(userId, accessToken);

            if(cartResponse != null && cartResponse.IsSuccess)
            {
               if(cartResponse.Result.CartHeader != null)
                {
                    foreach(CartDetailDto cartDetail in cartResponse.Result.CartDetails)
                    {
                        cartResponse.Result.CartHeader.OrderTotal += cartDetail.Product.Price * cartDetail.Count;
                    }

                    if (!string.IsNullOrEmpty(cartResponse.Result.CartHeader.CouponCode))
                    {
                        ResponseDto<CouponDto> coupon = await _couponService.GetAsync(cartResponse.Result.CartHeader.CouponCode, accessToken);

                        if (coupon != null && coupon.IsSuccess && coupon.Result != null)
                        {
                            if (double.TryParse(coupon.Result.DiscountAmount, out double discount))
                            {
                                double total = cartResponse.Result.CartHeader.OrderTotal;
                                cartResponse.Result.CartHeader.DiscountTotal = total * (discount / 100);
                                cartResponse.Result.CartHeader.OrderTotal = total - cartResponse.Result.CartHeader.DiscountTotal;
                                cartResponse.Result.CartHeader.Coupon = coupon.Result;
                            }
                        }
                    }

                    return cartResponse.Result;
                }
            }

            return null;
        }

        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            object response = await _cartService.Checkout(cartDto, await HttpContext.GetTokenAsync("access_token"));
            
            if(response is ResponseDto<bool>)
            {
                var responseDto = response as ResponseDto<bool>;

                if(!responseDto.IsSuccess)
                {
                    ViewBag.Error = responseDto.DisplayMessage;
                    return RedirectToAction(nameof(Checkout));
                }
            }

            return View(nameof(Confirmation));
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
