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

        public List<TblOrderDetail> GetAllOrders(int Id)
        {
            return _repo.GetOrderDetails(Id);
        }

        public void AddOrderDetail(TblOrderDetail orderDetail)
        {
            _repo.AddOrderDetail(orderDetail);
        }

        public void UpdateOrderDetail(TblOrderDetail orderDetail)
        {
            _repo.UpdateOrderDetail(orderDetail);
        }

        public void DeleteOrderDetail(TblOrderDetail orderDetail)
        {
            _repo.DeleteOrderDetail(orderDetail);
        }
    }
}
