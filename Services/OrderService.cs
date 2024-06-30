using Reprository.Models;
using Reprository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService
    {
        private OrderRepository _repo = new OrderRepository();

        public List<Order> GetAllOrders(int Id)
        {
            return _repo.GetOrders(Id);
        }

        public void AddOrder(Order order) 
        {
            _repo.AddOrder(order);
        }

        public void UpdateOrder(Order order) 
        {
            _repo.UpdateOrders(order);
        }

        public void DeleteOrder(Order order) 
        { 
            _repo.DeleteOrder(order);
        }
    }
}
