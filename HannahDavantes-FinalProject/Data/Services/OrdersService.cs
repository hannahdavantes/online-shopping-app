using HannahDavantes_FinalProject.Data.Utilities;
using HannahDavantes_FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.Services {
    public class OrdersService : IOrdersService {

        private readonly DbContextUtility _context;

        /// <summary>
        /// This constructor injects the DbContext
        /// </summary>
        /// <param name="context"></param>
        public OrdersService(DbContextUtility context) {
            _context = context;
        }

        /// <summary>
        /// This method will get the list of orders that the user has made
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole) {
            var orders = await _context.Orders.Include(n => n.OrderProducts).ThenInclude(n => n.Product).Include(n => n.User).ToListAsync();
            if(userRole != Roles.ADMIN) {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        /// <summary>
        /// This method will  add the order to the database
        /// </summary>
        /// <param name="products"></param>
        /// <param name="userId"></param>
        /// <param name="userEmailAddress"></param>
        /// <returns></returns>
        public async Task StoreOrderAsync(List<BasketProduct> products, string userId, string userEmailAddress) {
            var order = new Order() {
                UserId = userId,
                EmailAddress = userEmailAddress,
            };                                                                                                                                                                                           

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var product in products) {
                var orderProduct = new OrderProduct() {
                    Quantity = product.Quantity,
                    ProductId = product.Product.Id,
                    OrderId = order.Id,
                    Price = product.Product.Price
                };
                await _context.OrderProducts.AddAsync(orderProduct);
            }
            await _context.SaveChangesAsync();
        }
    }
}
