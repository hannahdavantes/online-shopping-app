using HannahDavantes_FinalProject.Data.Services;
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

        public IActionResult AddProduct() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProduct) {
            string fileName = null;
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
                newProduct.Photo = "uploads/" + fileName;
            }

            await _productsService.AddProductAsync(newProduct);
            TempData["SuccessMessage"] = "New Product Added Succesfully";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(int id) {
            var product = await _productsService.GetProductById(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProductContinue(int id) {
            var product = await _productsService.GetProductById(id);
            if (product == null) {
                return View("Invalid");
            }

            await _productsService.DeleteProductAsync(id);
            TempData["ErrorMessage"] = "Product Deleted Succesfully";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditProduct(int id) {
            var product = await _productsService.GetProductById(id);
            if (product == null) {
                return View("Invalid");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, [Bind("Id,Name,Brand,Category,SizeNumber,SizeUnit,PhotoFile,Description,Price,Photo")] Product product) {

            string fileName = null;

            if (!ModelState.IsValid) {
                return View(product);
            }

            if (product.PhotoFile != null) {
                string photoFolder = "img/uploads/";
                string fileExtension = Path.GetExtension(product.PhotoFile.FileName);
                fileName = Guid.NewGuid().ToString() + fileExtension;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, photoFolder + fileName);
                product.Photo = "uploads/" + fileName;

                await product.PhotoFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }

            
            await _productsService.UpdateProductAsync(id, product);
            TempData["SuccessMessage"] = "Product Updated Succesfully";

            return RedirectToAction(nameof(Index));
        }

    }
}
