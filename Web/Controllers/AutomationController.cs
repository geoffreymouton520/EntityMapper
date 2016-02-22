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
using Learning;
using Web.Models;

namespace Web.Controllers
{
    public class AutomationController : ApiController
    {

        //private readonly IRepository<Entity> _entityRepo;
        private readonly IMappingRepository _mappingRepository;


        public AutomationController( IMappingRepository mappingRepository)
        {
            //_entityRepo = entityRepo;
            _mappingRepository = mappingRepository;
        }
        // POST: api/PropertyMappings
        [ResponseType(typeof(EntityMappingViewModel))]
        public IHttpActionResult Automate([FromBody]AutomatedEntityMappingViewModel entityMapping)
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

                //var learningSession = new LearningSession(new UntrainedData
                //{
                //    Sources = _entityRepo.GetAll().Where(t => t.SystemId == entityMapping.SourceSystemId).Select(t => t.Name).ToList(),
                //    Destinations = _entityRepo.GetAll().Where(t => t.SystemId == entityMapping.DestinationSystemId).Select(t => t.Name).ToList()
                //});
                //var result = learningSession.StartLearning();


                var automatedMapping = new AutomatedMapping
                {
                    DestinationId = 177,
                    SourceId = 176,
                    LearningResult = new LearningResult(new List<LearntMapping>
                {
                    new LearntMapping
                    {
                        Mapping = new Mapping
                        {
                            Source = "qmob_commodity",
                            Destination = "qcommodity"
                        }
                    }
                })
                };

                //new AutomatedMapping
                //{
                //    LearningResult = result,
                //    SourceId = entityMapping.SourceSystemId,
                //    DestinationId = entityMapping.DestinationSystemId
                //}

                var newMappings = _mappingRepository.CreateMapping(automatedMapping);
                return Ok(Mapper.Map<IEnumerable<EntityMappingViewModel>>(newMappings));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
