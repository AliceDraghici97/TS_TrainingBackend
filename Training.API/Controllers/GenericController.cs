using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Training.IRepository;
using Training.Model.Entity;

namespace Training.API.Controllers
{
    //[Authorize]
    public abstract class GenericController<T> : ApiController where T : GenericEntity
    {
        private readonly IGenericRepository<T> _genericRepository;
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        public GenericController(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        
        public IHttpActionResult Get() => Ok(_genericRepository.Get());

        public IHttpActionResult Get(int id) => Ok(_genericRepository.GetById(id));

        [HttpPost]
        public IHttpActionResult Post(T entity)
        {
            try
            {
                _genericRepository.Add(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put( int id, T entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _genericRepository.Update(id, entity);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _genericRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
