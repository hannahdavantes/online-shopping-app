using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// This class represents a ViewComponent which counts the number of products in the basket and display it beside the basket icon
/// </summary>
namespace HannahDavantes_FinalProject.Data {
    public class BasketNavItem : ViewComponent {

        private readonly Basket _basket;

        public BasketNavItem(Basket basket) {
            _basket = basket;
        }

        public IViewComponentResult Invoke() {
            var products = _basket.GetBasketProducts();
            var numberOfProducts = products.Sum(n => n.Quantity);
            return View(numberOfProducts);
        }
    }
}
