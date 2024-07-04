using Reprository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reprository.Repositories
{
    public class ProductCategoriesRepository
    {
        private MilkShopDbContext _db;
        public List<Category> GetAll()
        {
            _db = new MilkShopDbContext();
            var categoryList = _db.Categories.ToList();
            return categoryList;
        }

    }
}
