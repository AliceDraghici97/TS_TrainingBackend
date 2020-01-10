using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Training.IRepository.Entity;
using Training.Model.Entity;

namespace Training.API.Controllers.Entity
{
   // [Authorize]
    public class StundetsXSubjectsController : GenericController<StudentsXSubject>
    {
        private readonly IStudentXSubjectRepository _studentXSubjectRepository;

        public StundetsXSubjectsController(IStudentXSubjectRepository studentXSubjectRepository) : base(studentXSubjectRepository)
        {
            _studentXSubjectRepository = studentXSubjectRepository;
        }

    }
}