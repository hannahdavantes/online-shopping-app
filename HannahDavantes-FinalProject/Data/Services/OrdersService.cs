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

        public OrdersService(DbContextUtility context) {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId) {
            var orders = await _context.Orders.Include(n => n.OrderProducts).ThenInclude(n => n.Product).Where(n => n.UserId == userId).ToListAsync();
            return orders;
        }

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
