using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Model.Custom;
using Training.Model.Entity;

namespace Training.IRepository.Entity
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        void AssignToSubject(int studentId, int subjectId);

        void AssignToSubject(AssignToSubjectRequest assignToSubjectRequest);
    }
}
