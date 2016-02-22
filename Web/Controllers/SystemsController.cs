using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Data.Contracts;
using Web.Models;

namespace Web.Controllers
{
    public class SystemsController : ApiController
    {
        private readonly IRepository<global::Data.Models.System> _repository;


        public SystemsController(IRepository<global::Data.Models.System> repository)
        {
            _repository = repository;
        }

        // GET: api/Systems
        [ResponseType(typeof(SystemViewModel))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll().Include(t => t.Domain).Select(Mapper.Map<SystemViewModel>));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Systems
        [ResponseType(typeof(SystemViewModel))]
        public IHttpActionResult GetByDomain(int domainId)
        {
            try
            {
                var systemViewModels = _repository.GetAll().Include(t => t.Domain).Where(t => t.DomainId == domainId).Select(Mapper.Map<SystemViewModel>).ToList();
                return Ok(systemViewModels);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: api/Systems/5
        [ResponseType(typeof(SystemViewModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                global::Data.Models.System system;
                if (id > 0)
                {
                    system = _repository.GetAll().Include(t => t.Domain).First(t => t.Id == id);

                    if (system == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    system = new global::Data.Models.System();
                }
                return Ok(Mapper.Map<SystemViewModel>(system));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Systems
        [ResponseType(typeof(SystemViewModel))]
        public IHttpActionResult Post([FromBody]SystemViewModel system)
        {
            try
            {
                if (system == null)
                {
                    return BadRequest("System cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newSystem = _repository.Add(Mapper.Map<global::Data.Models.System>(system));

                if (newSystem == null)
                {
                    return Conflict();
                }
                return Created(Request.RequestUri + newSystem.Id.ToString(), Mapper.Map<SystemViewModel>(newSystem));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Systems/5
        [ResponseType(typeof(SystemViewModel))]
        public IHttpActionResult Put(int id, [FromBody]SystemViewModel system)
        {
            try
            {
                if (system == null)
                {
                    return BadRequest("System cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedSystem = _repository.Update(Mapper.Map<global::Data.Models.System>(system));
                if (updatedSystem == null)
                {
                    return NotFound();
                }
                return Ok(updatedSystem);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Systems/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
