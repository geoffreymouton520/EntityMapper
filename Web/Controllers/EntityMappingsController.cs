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
using Learning;
using Web.Models;

namespace Web.Controllers
{
    public class EntityMappingsController : ApiController
    {
        private readonly IRepository<EntityMapping> _repository;
        private readonly IRepository<MappingOrigin> _originRepo;
        private readonly IRepository<Entity> _entityRepo;
        private readonly IMappingRepository _mappingRepository;


        public EntityMappingsController(IRepository<EntityMapping> repository, IRepository<MappingOrigin> originRepo, IRepository<Entity> entityRepo, IMappingRepository mappingRepository)
        {
            _repository = repository;
            _originRepo = originRepo;
            _entityRepo = entityRepo;
            _mappingRepository = mappingRepository;
        }

        // GET: api/PropertyMappings
        [ResponseType(typeof(EntityMappingViewModel))]
        public IHttpActionResult Get()
        {
            try
            {
                var entityMappingViewModels = _repository.GetAll()
                    .Include(t => t.Source)
                    .Include(t => t.Destination)
                    .Include(t => t.MappingOrigin)
                    .Include(t => t.Source.System)
                    .Include(t => t.Destination.System)
                    .Include(t => t.Destination.System)
                    .Select(Mapper.Map<EntityMappingViewModel>).ToList();
                return Ok(entityMappingViewModels);
                //return Ok(new List<EntityMappingViewModel>
                //{
                //    new EntityMappingViewModel
                //    {
                //        SourceEntityId = 1,
                //        Source = "Labour",
                //        DestinationEntityId = 2,
                //        Destination = "Quality Control",
                //        MappingOriginId = 1,
                //        MappingOrigin = "Manual",
                //        Confirmed = true,
                //        ConfirmedStatus = "Confirmed",
                //        ConfirmedLabel = "success",
                //        Correct = true,
                //        CorrectStatus = "Correct",
                //        CorrectLabel = "success",
                //        Id = 1
                //        }
                //});
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: api/PropertyMappings/5
        [ResponseType(typeof(EntityMappingViewModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                EntityMapping entityMapping;
                if (id > 0)
                {

                    entityMapping = _repository.GetAll()
                    .Include(t => t.Source)
                    .Include(t => t.Destination)
                    .Include(t => t.MappingOrigin)
                    .Include(t => t.Source.System)
                    .Include(t => t.Destination.System)
                    .Include(t => t.Destination.System).First(t => t.Id == id);

                    if (entityMapping == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    entityMapping = new EntityMapping();
                }
                return Ok(Mapper.Map<EntityMappingViewModel>(entityMapping));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/PropertyMappings
        [ResponseType(typeof(EntityMappingViewModel))]
        public IHttpActionResult Post([FromBody]EntityMappingViewModel entityMapping)
        {
            try
            {
                if (entityMapping == null)
                {
                    return BadRequest("EntityMapping cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var manualMapping = _originRepo.GetAll().First(t => t.Name.Equals("Manual"));
                entityMapping.MappingOriginId = manualMapping.Id;
                entityMapping.Confirmed = true;
                entityMapping.Correct = true;
                var dbModel = Mapper.Map<EntityMapping>(entityMapping);
                var newEntityMapping = _repository.Add(dbModel);

                if (newEntityMapping == null)
                {
                    return Conflict();
                }
                return Created(Request.RequestUri + newEntityMapping.Id.ToString(), Mapper.Map<EntityMappingViewModel>(newEntityMapping));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        // PUT: api/PropertyMappings/5
        [ResponseType(typeof(EntityMappingViewModel))]
        public IHttpActionResult Put(int id, [FromBody]EntityMappingViewModel entityMapping)
        {
            try
            {
                if (entityMapping == null)
                {
                    return BadRequest("EntityMapping cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedEntityMapping = _repository.Update(Mapper.Map<EntityMapping>(entityMapping));
                if (updatedEntityMapping == null)
                {
                    return NotFound();
                }
                return Ok(updatedEntityMapping);
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
