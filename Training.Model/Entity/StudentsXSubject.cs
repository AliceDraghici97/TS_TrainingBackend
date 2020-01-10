using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Model.Entity
{
    public class StudentsXSubject : GenericEntity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public decimal Grade { get; set; }
    }
}
