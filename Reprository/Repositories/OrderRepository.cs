using Microsoft.EntityFrameworkCore;
using Reprository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reprository.Repositories
{
    public class OrderRepository
    {
        private MilkShopDbContext _context;

        public List<Order> GetOrders(int Id)
        {
            _context = new();
            return _context.Orders.Where(x => x.UserId == Id).ToList();
        } 

        public void UpdateOrders(Order order)
        {
            _context = new();

            var tracker = _context.Attach(order);
            tracker.State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            _context = new();
            _context.Orders.Add(order);

            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context = new();
            _context.Orders.Remove(order);

            _context.SaveChanges();
        }

    }
}
