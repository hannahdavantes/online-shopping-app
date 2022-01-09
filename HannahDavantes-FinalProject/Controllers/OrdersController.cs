using HannahDavantes_FinalProject.Data.Services;
using HannahDavantes_FinalProject.Data.ViewModel;
using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Controllers {
    public class OrdersController : Controller {

        private readonly IProductsService _productsService;
        private readonly Basket _basket;
        private readonly IOrdersService _ordersService;

        /// <summary>
        /// Controller that injects the following services: ProductsService, Baket and OrdersService
        /// </summary>
        /// <param name="productsService"></param>
        /// <param name="basket"></param>
        /// <param name="ordersService"></param>
        public OrdersController(IProductsService productsService, Basket basket, IOrdersService ordersService) {
            _productsService = productsService;
            _basket = basket;
            _ordersService = ordersService;
        }

        /// <summary>
        /// This shows the list of orders made by the user (if a user is logged in) or the list of orders of all users (if an admin is logged in)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Index() {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        /// <summary>
        /// This method will show the My Basket page
        /// </summary>
        /// <returns></returns>
        public IActionResult MyBasket() {

            var basketProducts = _basket.GetBasketProducts();
            _basket.BasketProducts = basketProducts;

            var basketViewModel = new BasketViewModel() {
                Basket = _basket,
                BasketTotalPrice = _basket.GetBasketTotalPrice()
            };

            return View(basketViewModel);
        }

        /// <summary>
        /// This method will add the product to the basket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddToBasket(int id) {
            var product = await _productsService.GetProductById(id);
            if (product != null) {
                _basket.AddProductToBasket(product);
            }
            return RedirectToAction(nameof(MyBasket));
        }
        
        /// <summary>
        /// This method will decrement the quantity of product if quantity is more than 1 or delete the product from the basket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveFromBasket(int id) {
            var product = await _productsService.GetProductById(id);
            if (product != null) {
                _basket.RemoveProductFromCart(product);
            }

            return RedirectToAction(nameof(MyBasket));

        }

        /// <summary>
        /// This method will checkout the products from user's basket
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Checkout() {
            var products = _basket.GetBasketProducts();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(products, userId, userEmailAddress);
            await _basket.ClearBasketAsync();

            return View("OrderCompleted");
        }
    }
}
