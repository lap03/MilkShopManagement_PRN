using Reprository.Models;
using Reprository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AdminService
    {
        private static readonly UserRepository _userRepository = new UserRepository();

        private static readonly OrderRepository _orderRepository = new OrderRepository();
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll(); //phân trang sort trước khi trả về ...
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll(); 
        }

        public void DeleteOrder(Order order)
        {
            _orderRepository.DeleteOrder(order);
        }

        public void DeleteUser(User user)
        {
            if (user != null)
            {
                _userRepository.Delete(user);
            }
        }

        public void AddUser(User user)
        {
            if (user != null)
            {
                _userRepository.Add(user);
            }
        }

        public void SaveUser(User user)
        {
            if (user != null)
            {
                _userRepository.Update(user);
            }
        }
    }

   
}
