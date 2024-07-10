using Reprository.Models;
using Reprository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Services
{
    public class ProductService
    {
        private ProductRepository _repo = new ProductRepository();

        //hàm dùng để làm bên Order với OrderDetail kh cần quan tâm
        public List<Product> GetAllProducts()
        {
            return _repo.GetProducts();
        }

        public List<Product> GetAllProductsWithIsActive()
        {
            return _repo.GetProducts().Where(p => p.IsActive == true).ToList();
        }

        public void AddProduct(Product product)
        {
            _repo.CreateProduct(product);
        }
        public void RemoveProduct(int id)
        {
            _repo.DeleteProduct(id);

        }
        public void UpdateProduct(Product product)
        {
            _repo.UpdateProduct(product);
        }
        public List<Product> SearchProductByKeywordAndPriceRange(string keyword, double? minPrice, double? maxPrice)
        {
            var query = _repo.GetProducts().AsQueryable();

            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword) || x.Description.ToLower().Contains(keyword));
            }

            // Filter by price range if both minPrice and maxPrice are provided
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                if (minPrice > maxPrice)
                {
                    double temp = minPrice.Value;
                    minPrice = maxPrice;
                    maxPrice = temp;
                }

                query = query.Where(p => p.Price >= minPrice.Value && p.Price <= maxPrice.Value);
            }

            return query.ToList();
        }
    }
}
