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
        private OrderDetailRepository _orderDetailRepository = new OrderDetailRepository();
        private ProductRepository _productRepository = new ProductRepository();

        public List<Order> GetAllOrders(int Id)
        {
            return _repo.GetOrders(Id);
        }

        public List<Order> GetAllOrdersIsActive(int Id)
        {
            return _repo.GetOrders(Id).Where(o => o.IsActive == true).ToList();
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
            handleQuantityDeleteOrder(order);
            _repo.DeleteOrder(order);
        }

        private void handleQuantityDeleteOrder(Order order)
        {
            var list = _orderDetailRepository.GetOrderDetails(order.OrderId);
            foreach (var item in list)
            {
                var existingProduct = _productRepository.GetProductById(item.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.QuantityInStock += item.Quantity.GetValueOrDefault(0);
                    _productRepository.UpdateProduct(existingProduct);
                }
            }
        }
    }
}
