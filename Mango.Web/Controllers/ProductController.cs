using Mango.Web.Models;
using Mango.Web.Service;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService ProductService)
        {
            this.productService = ProductService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = null;
            ResponceDto? ProductDto = await productService.GetAllProductsAsync();
            if(ProductDto != null && ProductDto.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(ProductDto.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponceDto? response = await productService.CreateProductsAsync(model);

                if (response != null && response.isSuccess)
                {
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int ProductId)
        {
            ResponceDto? response = await productService.GetProductByIdAsync(ProductId);

            if (response != null && response.isSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto ProductDto)
        {
            ResponceDto? response = await productService.DeleteProductsAsync(ProductDto.ProductId);

            if (response != null && response.isSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(ProductDto);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponceDto? response = await productService.GetProductByIdAsync(productId);

            if (response != null && response.isSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponceDto? response = await productService.UpdateProductsAsync(productDto);

                if (response != null && response.isSuccess)
                {
                    TempData["success"] = "Product updated successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(productDto);
        }
    }
}
