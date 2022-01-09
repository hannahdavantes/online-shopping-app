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

        /// <summary>
        /// This constructor injects the DbContext service
        /// </summary>
        /// <param name="dbContextUtility"></param>
        public ProductsService(DbContextUtility dbContextUtility) {
            _dbContextUtility = dbContextUtility;
        }
        /// <summary>
        /// This method will add product to Products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task AddProductAsync(Product product) {
            await _dbContextUtility.Products.AddAsync(product);
            await _dbContextUtility.SaveChangesAsync();
        }
        /// <summary>
        /// This method will remove product from Products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteProductAsync(int id) {
            var result = await _dbContextUtility.Products.FirstOrDefaultAsync(n => n.Id == id);
            _dbContextUtility.Products.Remove(result);
            await _dbContextUtility.SaveChangesAsync();
        }
        /// <summary>
        /// This method will get the list of all products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync() {
            var productsList = await _dbContextUtility.Products.ToListAsync();
            return productsList;
        }

        /// <summary>
        /// This method will return a product based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductById(int id) {
            var product = await _dbContextUtility.Products.FirstOrDefaultAsync(product => product.Id == id);
            return product;
        }

        /// <summary>
        /// This method will update the details of product based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> UpdateProductAsync(int id, Product product) {
            _dbContextUtility.Update(product);
            await _dbContextUtility.SaveChangesAsync();
            return product;
        }
    }
}
