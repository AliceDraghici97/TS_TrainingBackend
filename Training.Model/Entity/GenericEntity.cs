using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Model.Entity
{
    public class GenericEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } 
        public DateTime? ModifiedOn { get; set; }

        public User user;

        public DateTime? CreatedBy;

        public DateTime? ModifiedBy;

    }
}
