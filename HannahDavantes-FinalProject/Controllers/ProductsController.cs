using HannahDavantes_FinalProject.Data.Services;
using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Controllers {
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller {
        private readonly IProductsService _productsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// This contructor will inject the following services: ProductsServices and WebHostEnvironment
        /// </summary>
        /// <param name="productsService"></param>
        /// <param name="webHostEnvironment"></param>
        public ProductsController(IProductsService productsService, IWebHostEnvironment webHostEnvironment) {
            _productsService = productsService;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// This will show the page of the products
        /// The user and admin can both see details of the product and add them to the basket
        /// If an admin is logged in, the add, delete and edit buttons are shown
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index() {
            var productsList = await _productsService.GetAllProductsAsync();
            return View(productsList);
        }

        /// <summary>
        /// This will show the product description page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> ViewProductDetails(int id) {
            var product = await _productsService.GetProductById(id);
            return View(product);
        }

        /// <summary>
        /// This will show the add product form page
        /// </summary>
        /// <returns></returns>
        public IActionResult AddProduct() {
            return View();
        }

        /// <summary>
        /// This method will send a request to add the product to the database
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method will show the product deletion confirmation page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteProduct(int id) {
            var product = await _productsService.GetProductById(id);
            return View(product);
        }


        /// <summary>
        /// This method will send a request to delete the product from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// This method will return a page that contains a populated form of the product that the admin wants to edit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, [Bind("Id,Name,Brand,Category,SizeNumber,SizeUnit,PhotoFile,Description,Price,Photo")] Product product) {

            string fileName = null;

            if (!ModelState.IsValid) {
                return View(product);
            }

            //Checks if the photo selected a photo
            //If there is a photo then the application will copy the file and save it to the wwwwroot/img/uploads folder with its generated filename
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
