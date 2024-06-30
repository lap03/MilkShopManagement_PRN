using Reprository.Models;
using Reprository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
