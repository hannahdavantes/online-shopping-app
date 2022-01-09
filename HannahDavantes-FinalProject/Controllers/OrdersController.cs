using HannahDavantes_FinalProject.Data.Services;
using HannahDavantes_FinalProject.Data.ViewModel;
using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Controllers {
    public class OrdersController : Controller {

        private readonly IProductsService _productsService;
        private readonly Basket _basket;
        private readonly IOrdersService _ordersService;

        public OrdersController(IProductsService productsService, Basket basket, IOrdersService ordersService) {
            _productsService = productsService;
            _basket = basket;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index() {
            string userId = "";
            var orders = await _ordersService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        public IActionResult MyBasket() {

            var basketProducts = _basket.GetBasketProducts();
            _basket.BasketProducts = basketProducts;

            var basketViewModel = new BasketViewModel() {
                Basket = _basket,
                BasketTotalPrice = _basket.GetBasketTotalPrice()
            };

            return View(basketViewModel);
        }

        public async Task<IActionResult> AddToBasket(int id) {
            var product = await _productsService.GetProductById(id);
            if (product != null) {
                _basket.AddProductToBasket(product);
            }

            return RedirectToAction(nameof(MyBasket));

        }

        public async Task<IActionResult> RemoveFromBasket(int id) {
            var product = await _productsService.GetProductById(id);
            if (product != null) {
                _basket.RemoveProductFromCart(product);
            }

            return RedirectToAction(nameof(MyBasket));

        }

        public async Task<IActionResult> Checkout() {
            var products = _basket.GetBasketProducts();
            string userId = "";
            string userEmailAddress = "";

            await _ordersService.StoreOrderAsync(products, userId, userEmailAddress);
            await _basket.ClearBasketAsync();


            return View("OrderCompleted");
        }
    }
}
