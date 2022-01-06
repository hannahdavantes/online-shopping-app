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

        public Task AddProductAsync(Product product) {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() {
            var productsList = await _dbContextUtility.Products.ToListAsync();
            return productsList;
        }

        public Task<Product> GetProductById(int id) {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProductAsync(int id, Product product) {
            throw new NotImplementedException();
        }
    }
}
