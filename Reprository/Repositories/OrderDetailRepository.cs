using Microsoft.EntityFrameworkCore;
using Reprository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reprository.Repositories
{
    public class OrderDetailRepository
    {
        private MilkShopDbContext _context;

        public List<TblOrderDetail> GetOrderDetails(int Id)
        {
            _context = new();
            return _context.TblOrderDetails
                .Include(x=> x.Product)
                .Where(x => x.OrderId == Id).ToList();
        }

        public void UpdateOrderDetail(TblOrderDetail orderDetail)
        {
            _context = new();

            var tracker = _context.Attach(orderDetail);
            tracker.State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void AddOrderDetail(TblOrderDetail orderDetail)
        {
            _context = new();

            TblOrderDetail existingOrderDetail = _context.TblOrderDetails
                .FirstOrDefault(od => od.OrderId == orderDetail.OrderId && od.ProductId == orderDetail.ProductId);

            if (existingOrderDetail != null)
            {
                existingOrderDetail.Quantity += orderDetail.Quantity;
            }
            else
            {
                _context.TblOrderDetails.Add(orderDetail);
            }

            _context.SaveChanges();
        }

        public void DeleteOrderDetail(TblOrderDetail orderDetail)
        {
            _context = new();
            _context.TblOrderDetails.Remove(orderDetail);

            _context.SaveChanges();
        }
    }
}
