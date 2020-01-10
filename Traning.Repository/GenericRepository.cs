using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.IRepository;
using Training.Model.Entity;
using Traning.Repository.Helpers;

namespace Traning.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : GenericEntity
    {
        protected abstract string TableName { get; }

        protected abstract T ToEntity(DataRow dataRow);

        public IEnumerable<string> TableColumns
        {
            get
            {
                return typeof(T).GetProperties().Select(p => p.Name);
            }
        }

        protected IEnumerable<T> ToEntity(IEnumerable<DataRow> dataRows)
        {
            return dataRows.Select(dr => ToEntity(dr));
        }

        public virtual void Add(T entity)
        {
            entity.CreatedOn = DateTime.Now;
            List<string> values = GetPropertyValues(entity);
            SQLHelper.Insert(TableName, TableColumns, values);
        }

        public virtual void Delete(int id)
        {
            SQLHelper.Delete(TableName, id);
        }

        public virtual IEnumerable<T> Get()
        {
            IEnumerable<DataRow> dataRows = SQLHelper.Get(TableName);
            IEnumerable<T> entities = ToEntity(dataRows);
            return entities;
        }

        public virtual T GetById(int id)
        {
            DataRow dataRow = SQLHelper.GetById(TableName, id);
            T entity = ToEntity(dataRow);
            return entity;
        }

        public virtual void Update(int id, T entity)
        {
            entity.ModifiedOn = DateTime.Now;
            List<string> values = GetPropertyValues(entity);
            SQLHelper.Update(TableName, TableColumns, values, id);
        }

        public List<string> GetPropertyValues(T entity)
        {
            List<string> values = new List<string>();
            typeof(T).GetProperties().FirstOrDefault().GetValue(entity);
            foreach (string column in TableColumns)
            {
                object propertyValue = typeof(T).GetProperty(column).GetValue(entity);
                values.Add(propertyValue != null
                                 ? $"'{propertyValue.ToString()}'"
                                 : "null");                
            }
            return values;
        }
    }
}