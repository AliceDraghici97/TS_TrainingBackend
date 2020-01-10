using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.IRepository;
using Training.IRepository.Entity;
using Training.Model.Entity;

namespace Traning.Repository
{
    public class StudentXSubjectRepository : GenericRepository<StudentsXSubject>, IStudentXSubjectRepository
    {
        protected override string TableName { get => "StudentsXSubjects"; }

        protected override StudentsXSubject ToEntity(DataRow dataRow)
        {
            StudentsXSubject entity = new StudentsXSubject();
            entity.Id = dataRow.Field<int>("ID");
            entity.CreatedOn = dataRow.Field<DateTime>("CreatedOn");
            entity.ModifiedOn = dataRow.Field<DateTime?>("ModifiedOn");
            entity.StudentId = dataRow.Field<int>("StudentId");
            entity.SubjectId = dataRow.Field<int>("SubjectId");
            entity.Grade = dataRow.Field<decimal>("Grade");
            return entity;
        }
    }
}