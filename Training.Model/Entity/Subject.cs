﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Model.Entity
{
    public class Subject : GenericEntity
    {
        public string Description { get; set; }
        public int CoursesNo { get; set; }
    }
}
