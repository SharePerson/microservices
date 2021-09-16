using Mango.Services.ProductApi.DTO;
using Mango.Services.ProductApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.ProductApi.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ResponseDto<IEnumerable<ProductDto>>> Get()
        {
            ResponseDto<IEnumerable<ProductDto>> response = new();

            try
            {
                response.Result = await _productRepository.GetAll();
                response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDto<ProductDto>> Get(int id)
        {
            ResponseDto<ProductDto> response = new();

            try
            {
                response.Result = await _productRepository.Get(id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }

        [HttpPost]
        public async Task<ResponseDto<ProductDto>> Post([FromBody] ProductDto product)
        {
            ResponseDto<ProductDto> response = new();

            try
            {
                response.Result = await _productRepository.CreateUpdate(product);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }

        [HttpPut]
        public async Task<ResponseDto<ProductDto>> Put([FromBody] ProductDto product)
        {
            ResponseDto<ProductDto> response = new();

            try
            {
                response.Result = await _productRepository.CreateUpdate(product);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDto<bool>> Delete(int id)
        {
            ResponseDto<bool> response = new();

            try
            {
                response.Result = await _productRepository.Delete(id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }
    }
}
