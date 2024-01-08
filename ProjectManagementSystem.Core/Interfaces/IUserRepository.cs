using ProjectManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Core.Interfaces
{
    public interface IUserRepository
    {
        public User GetById(int userId);
        public void Add(User user);
        public void Update(User user);
        public void Delete(int userId);
    }
}
