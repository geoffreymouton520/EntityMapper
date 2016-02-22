using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Data.Contracts;
using Data.Models;
using Web.Models;

namespace Web.Controllers
{
    public class MappingOriginsController : ApiController
    {
        private readonly IRepository<MappingOrigin> _repository;


        public MappingOriginsController(IRepository<MappingOrigin> repository)
        {
            _repository = repository;
        }

        // GET: api/MappingOrigins
        [ResponseType(typeof(MappingOriginViewModel))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll().Select(Mapper.Map<MappingOriginViewModel>));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: api/MappingOrigins/5
        [ResponseType(typeof(MappingOriginViewModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                MappingOrigin mappingOrigin;
                if (id > 0)
                {

                    mappingOrigin = _repository.GetById(id);

                    if (mappingOrigin == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    mappingOrigin = new MappingOrigin();
                }
                return Ok(Mapper.Map<MappingOriginViewModel>(mappingOrigin));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/MappingOrigins
        [ResponseType(typeof(MappingOriginViewModel))]
        public IHttpActionResult Post([FromBody]MappingOriginViewModel mappingOrigin)
        {
            try
            {
                if (mappingOrigin == null)
                {
                    return BadRequest("MappingOrigin cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newMappingOrigin = _repository.Add(Mapper.Map<MappingOrigin>(mappingOrigin));

                if (newMappingOrigin == null)
                {
                    return Conflict();
                }
                return Created(Request.RequestUri + newMappingOrigin.Id.ToString(), Mapper.Map<MappingOriginViewModel>(newMappingOrigin));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/MappingOrigins/5
        [ResponseType(typeof(MappingOriginViewModel))]
        public IHttpActionResult Put(int id, [FromBody]MappingOriginViewModel mappingOrigin)
        {
            try
            {
                if (mappingOrigin == null)
                {
                    return BadRequest("MappingOrigin cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedMappingOrigin = _repository.Update(Mapper.Map<MappingOrigin>(mappingOrigin));
                if (updatedMappingOrigin == null)
                {
                    return NotFound();
                }
                return Ok(updatedMappingOrigin);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/MappingOrigins/5
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
