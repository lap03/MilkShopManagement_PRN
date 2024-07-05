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
        private MilkShopDbContext _context = new MilkShopDbContext();

        public List<Order> GetAll()
        {
            _context = new();
            return _context.Orders
               .ToList();
        }

        public List<Order> GetOrders(int Id)
        {
            _context = new();
            return _context.Orders
                .Where(x => x.UserId == Id)
                .Select(order => new Order
                {
                    OrderId = order.OrderId,
                    CreateDate = order.CreateDate,
                    Totalmoney = order.TblOrderDetails.Sum(detail => detail.Price * detail.Quantity.GetValueOrDefault(0)),
                    UserId = order.UserId,
                    IsActive = order.IsActive
                })
                .ToList();
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
