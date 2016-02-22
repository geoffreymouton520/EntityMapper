using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;

namespace Data
{
    public class EntityMappingRepository : IMappingRepository
    {
        private readonly IRepository<EntityMapping> _repository;
        private readonly ILogger _logger;
        private readonly IRepository<Entity> _entityRepository;
        private readonly IRepository<MappingOrigin> _originRepository;

        public EntityMappingRepository(IRepository<EntityMapping> repository,ILogger logger, IRepository<Entity> entityRepository,IRepository<MappingOrigin> originRepository )
        {
            _repository = repository;
            _logger = logger;
            _entityRepository = entityRepository;
            _originRepository = originRepository;
        }

        public TrainingData GetMappings(int sourceId, int destinationId)
        {
            return new TrainingData(
                _repository.GetAll().Where(
                    t => t.Source.SystemId == sourceId && t.Destination.SystemId == destinationId)
                    .Select(t => new Mapping
                    {
                        Source = t.Source.Name,
                        Destination = t.Destination.Name
                    }));
        }

        public UntrainedData GetUntrainedData(int sourceSystemId, int destinationSystemId)
        {
            return new UntrainedData
            {
                Sources =
                    _entityRepository.GetAll().Where(
                        t =>
                            t.SystemId == sourceSystemId &&
                            t.SourceMappings.All(r => r.Destination.SystemId != destinationSystemId))
                        .Select(t => t.Name),
                Destinations =
                    _entityRepository.GetAll().Where(
                        t =>
                            t.SystemId == destinationSystemId &&
                            t.DestinationMappings.All(r => r.SourceId == sourceSystemId)).Select(t => t.Name)
            };
        }

        public IEnumerable<EntityMapping> CreateMapping(ManualMapping manualMapping)
        {
           // var succesFulInserts = 0;
            var mappings = new List<EntityMapping>();
            var mappingOrigin = _originRepository.GetAll().First(t => t.Name.Equals("Manual"));
            foreach (var mapping in manualMapping.Mappings)
            {
                try
                {
                    var sourceEntity = _entityRepository.GetAll().First(t => t.SystemId == manualMapping.SourceId && t.Name.Equals(mapping.Source));
                    var destinationEntity = _entityRepository.GetAll().First(t => t.SystemId == manualMapping.DestinationId && t.Name.Equals(mapping.Destination));
                    var entityMapping = new EntityMapping
                    {
                        Source = sourceEntity,
                        SourceId = sourceEntity.Id,
                        Destination = destinationEntity,
                        DestinationId = destinationEntity.Id,
                        Confirmed = false,
                        Correct = null,
                        MappingOrigin = mappingOrigin,
                        MappingOriginId = mappingOrigin.Id
                    };
                    _repository.Add(entityMapping);
                    mappings.Add(entityMapping);
                    //succesFulInserts++;
                }
                catch (Exception ex)
                {
                    _logger.Write(ex);
                }
            }
            return mappings;
        }

        public IEnumerable<EntityMapping> CreateMapping(AutomatedMapping automatedMapping)
        {
           // var succesFulInserts = 0;
            var mappings = new List<EntityMapping>();
            var mappingOrigin = _originRepository.GetAll().First(t => t.Name.Equals("Automated"));
            foreach (var mapping in automatedMapping.LearningResult)
            {
                try
                {
                    var sourceEntity = _entityRepository.GetAll().First(t => t.SystemId == automatedMapping.SourceId && t.Name.Equals(mapping.Mapping.Source));
                    var destinationEntity = _entityRepository.GetAll().First(t => t.SystemId == automatedMapping.DestinationId && t.Name.Equals(mapping.Mapping.Destination));
                    var entityMapping = new EntityMapping
                    {
                        SourceId = sourceEntity.Id,
                        Destination = destinationEntity,
                        DestinationId = destinationEntity.Id,
                        Confirmed = false,
                        Correct = null,
                        MappingOriginId = mappingOrigin.Id
                    };
                    _repository.Add(entityMapping);
                    //succesFulInserts++;
                    mappings.Add(entityMapping);
                }
                catch (Exception ex)
                {
                    _logger.Write(ex);
                }
            }
            return mappings;
        }


        public void Dispose()
        {

        }
    }
}
