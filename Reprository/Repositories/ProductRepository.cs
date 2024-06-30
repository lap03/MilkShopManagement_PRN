using Reprository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reprository.Repositories
{
    public class ProductRepository
    {
        private MilkShopDbContext _context;

        //hàm dùng để làm bên Order với OrderDetail kh cần quan tâm
        public List<Product> GetProducts()
        {
            _context = new();
            return _context.Products.ToList();
        }
    }
}
