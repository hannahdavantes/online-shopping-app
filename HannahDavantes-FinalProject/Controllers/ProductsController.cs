using HannahDavantes_FinalProject.Data.Services;
using HannahDavantes_FinalProject.Data.ViewModel;
using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Controllers {
    public class ProductsController : Controller {
        private readonly IProductsService _productsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductsService productsService, IWebHostEnvironment webHostEnvironment) {
            _productsService = productsService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index() {
            var productsList = await _productsService.GetAllProductsAsync();
            return View(productsList);
        }

        public async Task<IActionResult> ViewProductDetails(int id) {
            var product = await _productsService.GetProductById(id);
            return View(product);
        }

        public IActionResult AddProduct(Product product) {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(NewProductViewModel newProduct) {
            string fileName = "default/new.jpg";
            if (!ModelState.IsValid) {
                return View(newProduct);
            }

            //Process imagefile
            if (newProduct.PhotoFile != null) {
                string photoFolder = "img/uploads/";
                string fileExtension = Path.GetExtension(newProduct.PhotoFile.FileName);
                fileName = Guid.NewGuid().ToString() + fileExtension;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, photoFolder + fileName);

                await newProduct.PhotoFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            Product product = new Product() {
                Name = newProduct.Name,
                Brand = newProduct.Brand,
                Category = newProduct.Category,
                SizeNumber = newProduct.SizeNumber,
                SizeUnit = newProduct.SizeUnit,
                Photo = "uploads/" + fileName,
                Description = newProduct.Description,
                Price = newProduct.Price
            };

            await _productsService.AddProductAsync(product);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(int id) {

            return View();
        }

    }
}
