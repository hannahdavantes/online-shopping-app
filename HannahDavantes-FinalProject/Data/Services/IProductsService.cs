using HannahDavantes_FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.Services {
    public interface IProductsService {

        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductById(int id);
        Task AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task DeleteProductAsync(int id);
    }
}
