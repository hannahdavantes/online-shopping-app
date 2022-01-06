using HannahDavantes_FinalProject.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Controllers {
    public class ProductsController : Controller {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService) {
            _productsService = productsService;
        }

        public async Task<IActionResult> Index() {
            var productsList = await _productsService.GetAllProductsAsync();
            return View(productsList);
        }

        public async Task<IActionResult> ViewProductDetails(int id) {
            var product = await _productsService.GetProductById(id);
            return View(product);
        }

        public async Task<IActionResult> AddProduct() {

            return View();
        }
        
    }
}
