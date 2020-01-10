using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Training.IRepository.Entity;
using Training.Model.Entity;

namespace Training.API.Controllers.Entity
{
    [Authorize]
    public class SubjectsController : GenericController<Subject>
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository) : base(subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
    }
}

