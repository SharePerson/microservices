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

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
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

                    return cartResponse.Result;
                }
            }

            return null;
        }
    }
}
