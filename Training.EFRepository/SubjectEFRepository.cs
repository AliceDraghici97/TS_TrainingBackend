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
    public class SubjectEFRepository : GenericEFRepository<Subject>, ISubjectRepository
    {
        private readonly TrainingDBContext _trainingDBContext;

        public SubjectEFRepository(TrainingDBContext trainingDBContext) : base(trainingDBContext)
        {
            _trainingDBContext = trainingDBContext;
        }
    }
}