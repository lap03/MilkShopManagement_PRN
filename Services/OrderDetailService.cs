using Reprository.Models;
using Reprository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderDetailService
    {
        private OrderDetailRepository _repo = new OrderDetailRepository();
        private ProductRepository _productRepository = new ProductRepository();

        public List<TblOrderDetail> GetAllOrders(int Id)
        {
            return _repo.GetOrderDetails(Id);
        }

        public void AddOrderDetail(TblOrderDetail orderDetail)
        {
            handleQuantityAddOrderDetail(orderDetail);
            _repo.AddOrderDetail(orderDetail);
        }

        public void UpdateOrderDetail(TblOrderDetail orderDetail, int oldQuantity)
        {
            handleQuantityUpdateOrderDetail(orderDetail, oldQuantity);
            _repo.UpdateOrderDetail(orderDetail);
        }

        public void DeleteOrderDetail(TblOrderDetail orderDetail)
        {
            handleQuantityDeleteOrderDetail(orderDetail);
            _repo.DeleteOrderDetail(orderDetail);
        }

        private void handleQuantityAddOrderDetail(TblOrderDetail orderDetail)
        {
            var existingProduct = _productRepository.GetProductById(orderDetail.ProductId);
            if (existingProduct != null)
            {
                existingProduct.QuantityInStock -= orderDetail.Quantity.GetValueOrDefault(0);
                _productRepository.UpdateProduct(existingProduct);
            }
        }

        private void handleQuantityUpdateOrderDetail(TblOrderDetail orderDetail, int oldQuantity)
        {
            var existingProduct = _productRepository.GetProductById(orderDetail.ProductId);
            if (existingProduct != null)
            {
                existingProduct.QuantityInStock += oldQuantity;
                existingProduct.QuantityInStock -= orderDetail.Quantity.GetValueOrDefault(0);
                _productRepository.UpdateProduct(existingProduct);
            }
        }

        private void handleQuantityDeleteOrderDetail(TblOrderDetail orderDetail)
        {
            var existingProduct = _productRepository.GetProductById(orderDetail.ProductId);
            if (existingProduct != null)
            {
                existingProduct.QuantityInStock += orderDetail.Quantity.GetValueOrDefault(0);
                _productRepository.UpdateProduct(existingProduct);
            }
        }
    }
}
