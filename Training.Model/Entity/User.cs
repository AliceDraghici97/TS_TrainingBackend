using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Model.Entity;

namespace Training.Model
{
    public class User : GenericEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
