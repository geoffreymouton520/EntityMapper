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
using Data.Models;
using Web.Models;

namespace Web.Controllers
{
    public class EntitiesController : ApiController
    {
        private readonly IRepository<Entity> _repository;


        public EntitiesController(IRepository<Entity> repository)
        {
            _repository = repository;
        }

        // GET: api/Entities
        [ResponseType(typeof(EntityViewModel))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll().Include(t => t.System).Select(Mapper.Map<EntityViewModel>));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // GET: api/Entities
        [ResponseType(typeof(EntityViewModel))]
        public IHttpActionResult GetBySystem(int systemId)
        {
            try
            {
                var entityViewModels = _repository.GetAll().Include(t => t.System).Select(Mapper.Map<EntityViewModel>).Where(t => t.SystemId == systemId).ToList();
                return Ok(entityViewModels);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: api/Entities/5
        [ResponseType(typeof(EntityViewModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Entity entity;
                if (id > 0)
                {

                    entity = _repository.GetAll().Include(t => t.System).First(t => t.Id == id);

                    if (entity == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    entity = new Entity();
                }
                return Ok(Mapper.Map<EntityViewModel>(entity));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Entities
        [ResponseType(typeof(EntityViewModel))]
        public IHttpActionResult Post([FromBody]EntityViewModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest("Entity cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newEntity = _repository.Add(Mapper.Map<Entity>(entity));

                if (newEntity == null)
                {
                    return Conflict();
                }
                return Created(Request.RequestUri + newEntity.Id.ToString(), Mapper.Map<EntityViewModel>(newEntity));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Entities/5
        [ResponseType(typeof(EntityViewModel))]
        public IHttpActionResult Put(int id, [FromBody]EntityViewModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest("Entity cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedEntity = _repository.Update(Mapper.Map<Entity>(entity));
                if (updatedEntity == null)
                {
                    return NotFound();
                }
                return Ok(updatedEntity);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Entities/5
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
