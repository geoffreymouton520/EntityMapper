using System;
using System.Collections.Generic;
using Data.Contracts;
using Data.Models;

namespace Data
{
    public class PropertyMappingRepository : IMappingRepository
    {
        public TrainingData GetMappings(int sourceId, int destinationId)
        {
            throw new NotImplementedException();
        }

        public UntrainedData GetUntrainedData(int sourceSystemId, int destinationSystemId)
        {
            throw new NotImplementedException();
        }

        public int CreateMapping(ManualMapping manualMapping)
        {
            throw new NotImplementedException();
        }

        public int CreateMapping(AutomatedMapping automatedMapping)
        {
            throw new NotImplementedException();
        }

        IEnumerable<EntityMapping> IMappingRepository.CreateMapping(ManualMapping manualMapping)
        {
            throw new NotImplementedException();
        }

        IEnumerable<EntityMapping> IMappingRepository.CreateMapping(AutomatedMapping automatedMapping)
        {
            throw new NotImplementedException();
        }
    }
}