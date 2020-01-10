using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DBContext;
using Training.IRepository.Entity;
using Training.Model;

namespace Training.EFRepository
{
    public class UserEFRepository : GenericEFRepository<User>, IUserRepository
    {
        private readonly TrainingDBContext _trainingDBContext;

        public UserEFRepository(TrainingDBContext trainingDBContext) : base(trainingDBContext)
        {
            _trainingDBContext = trainingDBContext;
        }

        public User GetByCredentials(string username, string password)
        {
            User user = _trainingDBContext.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user;
        }

    }
}