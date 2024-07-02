using Reprository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reprository.Repositories
{
    public class UserRepository
    {
        private static readonly MilkShopDbContext _dbContext = new MilkShopDbContext();
        public User? Get(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        public List<User> GetAll() 
        {
            return _dbContext.Users.ToList();
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
