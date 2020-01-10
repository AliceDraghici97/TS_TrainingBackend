using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Model.Entity;

namespace Training.IRepository
{
    public interface IGenericRepository<T> where T : GenericEntity
    {
        void Add(T entity);

        void Update(int id, T entity);

        void Delete(int id);

        T GetById(int id);

        IEnumerable<T> Get();
    }
}
