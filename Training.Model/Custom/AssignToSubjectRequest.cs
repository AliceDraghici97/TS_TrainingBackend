using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Model.Custom
{
    public class AssignToSubjectRequest
    {
        public int StudentId { get; set; }

        public int SubjectId { get; set; }
    }
}
