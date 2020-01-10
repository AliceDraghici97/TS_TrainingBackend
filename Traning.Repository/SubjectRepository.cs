using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.IRepository;
using Training.IRepository.Entity;
using Training.Model.Entity;

namespace Traning.Repository
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        protected override string TableName => "Subjects";

        public override void Add(Subject entity)
        {
            base.Add(entity);
        }

        public override void Update(int id, Subject entity)
        {
            base.Update(id, entity);
        }

        protected override Subject ToEntity(DataRow dataRow)
        {
            Subject entity = new Subject();
            entity.Id = dataRow.Field<int>("ID");
            entity.CreatedOn = dataRow.Field<DateTime>("CreatedOn");
            entity.ModifiedOn = dataRow.Field<DateTime?>("ModifiedOn");
            entity.Description = dataRow.Field<string>("Description");
            return entity;
        }
    }
}
