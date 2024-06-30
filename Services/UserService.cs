using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Reprository.Models;
using Reprository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        private static readonly UserRepository _userRepository = new UserRepository();
        public User? checkLogin(string email, string password)
        {
            User user = _userRepository.Get(email);
            if (user != null)
            {
                if(user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
