using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> products = new();
            ResponseDto<IEnumerable<ProductDto>> response = await _productService.GetAllAsync(await HttpContext.GetTokenAsync("access_token"));
            if(response != null && response.IsSuccess)
            {
                products = response.Result.ToList();
            }
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto product)
        {
            if(ModelState.IsValid)
            {
                ResponseDto<ProductDto> response = await _productService.CreateAsync(product, await HttpContext.GetTokenAsync("access_token"));

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int productId)
        {
            ResponseDto<ProductDto> response = await _productService.GetAsync(productId, await HttpContext.GetTokenAsync("access_token"));

            if (response != null && response.IsSuccess)
            {
                return View(response.Result);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                ResponseDto<ProductDto> response = await _productService.UpdateAsync(product, await HttpContext.GetTokenAsync("access_token"));

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int productId)
        {
            ResponseDto<ProductDto> response = await _productService.GetAsync(productId, await HttpContext.GetTokenAsync("access_token"));

            if (response != null && response.IsSuccess)
            {
                return View(response.Result);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductDto product)
        {
            ResponseDto<bool> response = await _productService.DeleteAsync(product.Id, await HttpContext.GetTokenAsync("access_token"));

            if (response != null && response.IsSuccess && response.Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
    }
}
