using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Model;

namespace Training.IRepository.Entity
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByCredentials(string username, string password);
    }
}
