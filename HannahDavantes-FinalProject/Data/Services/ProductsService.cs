using HannahDavantes_FinalProject.Data.Utilities;
using HannahDavantes_FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.Services {
    public class ProductsService : IProductsService {
        private readonly DbContextUtility _dbContextUtility;

        public ProductsService(DbContextUtility dbContextUtility) {
            _dbContextUtility = dbContextUtility;
        }

        public async Task AddProductAsync(Product product) {
            await _dbContextUtility.Products.AddAsync(product);
            await _dbContextUtility.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id) {
            var result = await _dbContextUtility.Products.FirstOrDefaultAsync(n => n.Id == id);
            _dbContextUtility.Products.Remove(result);
            await _dbContextUtility.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() {
            var productsList = await _dbContextUtility.Products.ToListAsync();
            return productsList;
        }

        public async Task<Product> GetProductById(int id) {
            var product = await _dbContextUtility.Products.FirstOrDefaultAsync(product => product.Id == id);
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product) {
            _dbContextUtility.Update(product);
            await _dbContextUtility.SaveChangesAsync();
            return product;
        }
    }
}
