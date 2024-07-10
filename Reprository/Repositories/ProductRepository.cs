using Microsoft.EntityFrameworkCore;
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
            var products = _context.Products.Include(x => x.Category).ToList();
            return products;
        }
        public void UpdateProduct(Product product)
        {
            var trackedProduct = _context.Products.Local.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (trackedProduct == null)
            {

                _context.Products.Attach(product);
            }
            else
            {
                // Update the properties of the tracked entity

                _context.Entry(trackedProduct).CurrentValues.SetValues(product);
            }
            _context.SaveChanges();
          

        }
        public void CreateProduct(Product product)
        {
            _context = new();
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            _context = new();
            var product = _context.Products.Include(x => x.TblOrderDetails).FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                //if (product.TblOrderDetails.Count > 0)
                //{
                //    _context.Products.RemoveRange((Product)product.TblOrderDetails);
                //}
                product.IsActive = false;
             
                _context.SaveChanges();
            }
        }


    }
}
