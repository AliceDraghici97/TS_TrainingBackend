using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.IRepository;
using Training.IRepository.Entity;
using Training.Model.Custom;
using Training.Model.Entity;

namespace Traning.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        protected override string TableName => "Students";
        private readonly StudentXSubjectRepository _studentXSubjectRepository;

        public StudentRepository()
        {
            _studentXSubjectRepository = new StudentXSubjectRepository();
        }

        public override void Add(Student entity)
        {
            base.Add(entity);
        }

        public override void Update(int id, Student entity)
        {
            base.Update(id, entity);
        }

        protected override Student ToEntity(DataRow dataRow)
        {
            Student entity = new Student();
            entity.Id = dataRow.Field<int>("ID");
            entity.CreatedOn = dataRow.Field<DateTime>("CreatedOn");
            entity.ModifiedOn = dataRow.Field<DateTime?>("ModifiedOn");
            entity.Name = dataRow.Field<string>("Name");
            entity.Surname = dataRow.Field<string>("Surname");
            entity.PhoneNo = dataRow.Field<string>("PhoneNo");
            return entity;
        }

        public void AssignToSubject(int studentId, int subjectId)
        {
            StudentsXSubject entity = new StudentsXSubject()
            {
                SubjectId = subjectId,
                StudentId = studentId
            };
            _studentXSubjectRepository.Add(entity);
        }

        public void AssignToSubject(AssignToSubjectRequest assignToSubjectRequest)
        {
            throw new NotImplementedException();
        }
    }
}