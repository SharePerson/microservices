using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> products = new();
            ResponseDto<IEnumerable<ProductDto>> response = await _productService.GetAllAsync();
            if (response != null && response.IsSuccess)
            {
                products = response.Result.ToList();
            }
            return View(products);
        }

        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto product = new();
            ResponseDto<ProductDto> response = await _productService.GetAsync(productId, await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                product = response.Result;
            }
            return View(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(ProductDto productDto)
        {
            CartDto cartDto = new()
            {
                CartHeader = new()
                {
                    UserId = User.Claims.Where(u => u.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value
                }
            };

            CartDetailDto cartDetailDto = new()
            {
                Count = productDto.Count,
                ProductId = productDto.Id
            };

            string accessToken = await HttpContext.GetTokenAsync("access_token");

            ResponseDto<ProductDto> productResponse = await _productService.GetAsync(productDto.Id, accessToken);

            if(productResponse != null && productResponse.IsSuccess)
            {
                cartDetailDto.Product = productResponse.Result;
            }

            cartDto.CartDetails = new List<CartDetailDto> { cartDetailDto };

            ResponseDto<CartDto> cartResponse = await _cartService.CreateAsync(cartDto, accessToken);

            if(cartResponse != null && cartResponse.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
           
            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
