using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
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
            ResponseDto<IEnumerable<ProductDto>> response = await _productService.GetAllAsync();
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
                ResponseDto<ProductDto> response = await _productService.CreateAsync(product);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(product);
        }
    }
}
