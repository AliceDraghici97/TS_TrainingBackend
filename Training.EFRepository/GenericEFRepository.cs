using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DBContext;
using Training.IRepository;
using Training.Model.Entity;

namespace Training.EFRepository
{
    public abstract class GenericEFRepository<T> : IGenericRepository<T> where T : GenericEntity
    {
        private readonly TrainingDBContext _trainingDBContext;

        public GenericEFRepository(TrainingDBContext trainingDBContext)
        {
            _trainingDBContext = trainingDBContext;
        }

        public void Add(T entity)
        {
            _trainingDBContext.Set<T>().Add(entity);
            _trainingDBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = _trainingDBContext.Set<T>().FirstOrDefault(e => e.Id == id);
            _trainingDBContext.Set<T>().Remove(entity);
            _trainingDBContext.SaveChanges();
        }

        public IEnumerable<T> Get()
        {
            IEnumerable<T> list= _trainingDBContext.Set<T>();
            List<T> list2 = list.ToList();
            return list2;
        }

        public T GetById(int id)
        {
            return _trainingDBContext.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public void Update(int id, T entity)
        {
            entity.Id = id;
            _trainingDBContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;            
            _trainingDBContext.SaveChanges();
        }
    }
}