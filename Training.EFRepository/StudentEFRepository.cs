using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DBContext;
using Training.IRepository.Entity;
using Training.Model.Custom;
using Training.Model.Entity;

namespace Training.EFRepository
{
    public class StudentEFRepository : GenericEFRepository<Student>, IStudentRepository
    {
        private readonly TrainingDBContext _trainingDBContext;

        public StudentEFRepository(TrainingDBContext trainingDBContext) : base(trainingDBContext)
        {
            _trainingDBContext = trainingDBContext;
        }

        public void AssignToSubject(int studentId, int subjectId)
        {
            StudentsXSubject entity = new StudentsXSubject()
            {
                StudentId = studentId,
                SubjectId = subjectId
            };
            _trainingDBContext.StudentsXSubjects.Add(entity);
            _trainingDBContext.SaveChanges();
        }

        public void AssignToSubject(AssignToSubjectRequest assignToSubjectRequest)
        {
            AssignToSubject(assignToSubjectRequest.StudentId, assignToSubjectRequest.SubjectId);         
        }
    }
}