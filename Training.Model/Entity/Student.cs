using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Model.Entity
{
    public class Student : GenericEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNo { get; set; }

    }
}
