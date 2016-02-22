using System.Collections.Generic;
using Data.Models;

namespace Data.Contracts
{
    public interface IMappingRepository
    {
        TrainingData GetMappings(int sourceId, int destinationId);
        UntrainedData GetUntrainedData(int sourceSystemId, int destinationSystemId);
        IEnumerable<EntityMapping> CreateMapping(ManualMapping manualMapping);
        IEnumerable<EntityMapping> CreateMapping(AutomatedMapping automatedMapping);
    }
}