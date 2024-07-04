using Reprository.Models;
using Reprository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class productCategoriesService
    {
        private ProductCategoriesRepository _repo;
        public List<Category> getAllCategories() {

            _repo = new ProductCategoriesRepository();
            return _repo.GetAll()   ;
        }
    }
}
