using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Data.Contracts;
using Web.Models;
using System.Data.Entity;
using Data.Models;

namespace Web.Controllers
{
    public class PropertiesController : ApiController
    {
        private readonly IRepository<Property> _repository;


        public PropertiesController(IRepository<Property> repository)
        {
            _repository = repository;
        }

        // GET: api/Properties
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll().Include(t => t.Entity).Select(Mapper.Map<PropertyViewModel>));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: api/Properties/5
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Property property;
                if (id > 0)
                {

                    property = _repository.GetAll().Include(t => t.Entity).First(t => t.Id == id);

                    if (property == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    property = new Property();
                }
                return Ok(Mapper.Map<PropertyViewModel>(property));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Properties
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult Post([FromBody]PropertyViewModel property)
        {
            try
            {
                if (property == null)
                {
                    return BadRequest("Property cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newProperty = _repository.Add(Mapper.Map<Property>(property));

                if (newProperty == null)
                {
                    return Conflict();
                }
                return Created(Request.RequestUri + newProperty.Id.ToString(), Mapper.Map<PropertyViewModel>(newProperty));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Properties/5
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult Put(int id, [FromBody]PropertyViewModel property)
        {
            try
            {
                if (property == null)
                {
                    return BadRequest("Property cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedProperty = _repository.Update(Mapper.Map<Property>(property));
                if (updatedProperty == null)
                {
                    return NotFound();
                }
                return Ok(updatedProperty);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Properties/5
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
