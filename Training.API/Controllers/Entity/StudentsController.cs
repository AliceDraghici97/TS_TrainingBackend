using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Training.IRepository.Entity;
using Training.Model.Custom;
using Training.Model.Entity;

namespace Training.API.Controllers.Entity
{
    [Authorize]
    [RoutePrefix("api/students")]
    public class StudentsController : GenericController<Student>
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository) : base(studentRepository)
        {            
            _studentRepository = studentRepository;
        }

        [HttpPost, Route("AssignToSubject(studentId={studentId},subjectId={subjectId})")]
        public IHttpActionResult AssignToSubject([FromUri] int studentId, [FromUri] int subjectId)
        {
            try
            {
                _studentRepository.AssignToSubject(studentId, subjectId);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
     
        [HttpPost]
        [Route("AssignToSubject")]
        public IHttpActionResult AssignToSubject([FromBody] AssignToSubjectRequest assignToSubjectRequest)
        {
            try
            {
                _studentRepository.AssignToSubject(assignToSubjectRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost,Route("TestPost")]
        public IHttpActionResult TestPost([FromBody] string value) => Ok();
        
    }
}
