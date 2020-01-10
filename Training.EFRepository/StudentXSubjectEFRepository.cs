using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DBContext;
using Training.IRepository.Entity;
using Training.Model.Entity;

namespace Training.EFRepository
{
    public class StudentXSubjectEFRepository : GenericEFRepository<StudentsXSubject>, IStudentXSubjectRepository
    {
        private readonly TrainingDBContext _trainingDBContext;

        public StudentXSubjectEFRepository(TrainingDBContext trainingDBContext) : base(trainingDBContext)
        {
            _trainingDBContext = trainingDBContext;
        }
    }
}
