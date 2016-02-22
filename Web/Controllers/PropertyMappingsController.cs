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
    public class PropertyMappingsController : ApiController
    {
        private readonly IRepository<PropertyMapping> _repository;


        public PropertyMappingsController(IRepository<PropertyMapping> repository)
        {
            _repository = repository;
        }

        // GET: api/PropertyMappings
        [ResponseType(typeof(PropertyMappingViewModel))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll().Select(Mapper.Map<PropertyMappingViewModel>));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: api/PropertyMappings/5
        [ResponseType(typeof(PropertyMappingViewModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                PropertyMapping propertyMapping;
                if (id > 0)
                {

                    propertyMapping = _repository.GetById(id);

                    if (propertyMapping == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    propertyMapping = new PropertyMapping();
                }
                return Ok(Mapper.Map<PropertyMappingViewModel>(propertyMapping));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/PropertyMappings
        [ResponseType(typeof(PropertyMappingViewModel))]
        public IHttpActionResult Post([FromBody]PropertyMappingViewModel propertyMapping)
        {
            try
            {
                if (propertyMapping == null)
                {
                    return BadRequest("PropertyMapping cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newPropertyMapping = _repository.Add(Mapper.Map<PropertyMapping>(propertyMapping));

                if (newPropertyMapping == null)
                {
                    return Conflict();
                }
                return Created(Request.RequestUri + newPropertyMapping.Id.ToString(), Mapper.Map<PropertyMappingViewModel>(newPropertyMapping));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/PropertyMappings/5
        [ResponseType(typeof(PropertyMappingViewModel))]
        public IHttpActionResult Put(int id, [FromBody]PropertyMappingViewModel propertyMapping)
        {
            try
            {
                if (propertyMapping == null)
                {
                    return BadRequest("PropertyMapping cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedPropertyMapping = _repository.Update(Mapper.Map<PropertyMapping>(propertyMapping));
                if (updatedPropertyMapping == null)
                {
                    return NotFound();
                }
                return Ok(updatedPropertyMapping);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/PropertyMappings/5
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
