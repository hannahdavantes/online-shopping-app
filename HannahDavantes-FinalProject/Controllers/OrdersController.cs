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

        private readonly IProductsService _service;
        private readonly Basket _basket;

        public OrdersController(IProductsService service, Basket basket) {
            _service = service;
            _basket = basket;
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
            var product = await _service.GetProductById(id);
            if (product != null) {
                _basket.AddProductToBasket(product);
            }

            return RedirectToAction(nameof(MyBasket));

        }

        public async Task<IActionResult> RemoveFromBasket(int id) {
            var product = await _service.GetProductById(id);
            if (product != null) {
                _basket.RemoveProductFromCart(product);
            }

            return RedirectToAction(nameof(MyBasket));

        }
    }
}
