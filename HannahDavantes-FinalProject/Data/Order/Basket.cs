using HannahDavantes_FinalProject.Data.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Models {
    public class Basket {
        public DbContextUtility _context { get; set; }
        public string BasketId { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }

        /// <summary>
        /// This injects the DbContext service
        /// </summary>
        /// <param name="context"></param>
        public Basket(DbContextUtility context) {
            _context = context;
        }

        /// <summary>
        /// This method checks if there is basket saved in the session
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static Basket GetBasket(IServiceProvider services) {
            //Check if there is already a basket in the session
            //If there is not then we create a new session with name of BasketId and generate a GUID as the basket's id
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<DbContextUtility>();

            string basketId = session.GetString("BasketId");
            if (basketId == null) {
                basketId = Guid.NewGuid().ToString();
            }

            session.SetString("BasketId", basketId);

            return new Basket(context) {
                BasketId = basketId
            };

        }

        /// <summary>
        /// This method will check if the product is already in the basket
        /// If it is not in the basket then it gets added to the basket
        /// If it is already in the basket, then the quantity of the products gets incremented
        /// </summary>
        /// <param name="product"></param>
        public void AddProductToBasket(Product product) {
            //Check if it's already in the basket
            //If it is not in the basket then we add it to the list
            //If it is already in the basket, then we just increase the amount
            var basketItem = _context.BasketProducts.FirstOrDefault(n => n.Product.Id == product.Id && n.BasketId == BasketId);
            if (basketItem == null) {
                basketItem = new BasketProduct() {
                    BasketId = BasketId,
                    Product = product,
                    Quantity = 1
                };
                _context.BasketProducts.Add(basketItem);
            } else {
                basketItem.Quantity++;
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// This method will check the quantity of the product
        /// If quantity is >1 then the quantity gets decremented
        /// If quantity = 1 then the products gets removed
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProductFromCart(Product product) {
            //Check if it's already in the basket
            //We check the quantity
            //If quantity is >1 then we just decrement the quantity
            //If quantity = 1 then we remove the item from basket
            var basketItem = _context.BasketProducts.FirstOrDefault(n => n.Product.Id == product.Id && n.BasketId == BasketId);
            if (basketItem != null) {
                if (basketItem.Quantity > 1) {
                    basketItem.Quantity--;
                } else {
                    _context.BasketProducts.Remove(basketItem);
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// This method will get the logged in user's basket
        /// </summary>
        /// <returns></returns>
        public List<BasketProduct> GetBasketProducts() {
            BasketProducts = _context.BasketProducts.Where(n => n.BasketId == BasketId).Include(n => n.Product).ToList();
            if (BasketProducts != null) {
                return BasketProducts;
            } else {
                return new List<BasketProduct>();
            }
        }

        /// <summary>
        /// This method will get the total price of the basket
        /// </summary>
        /// <returns></returns>
        public double GetBasketTotalPrice() {
            var totalPrice = _context.BasketProducts.Where(n => n.BasketId == BasketId).Select(n => n.Product.Price * n.Quantity).Sum();
            return totalPrice;
        }

        public async Task ClearBasketAsync() {
            var products = await _context.BasketProducts.Where(n => n.BasketId == BasketId).ToListAsync();
            _context.BasketProducts.RemoveRange(products);
            await _context.SaveChangesAsync();
        }

    }
}
