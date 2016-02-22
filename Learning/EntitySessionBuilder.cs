using Data.Contracts;
using Data.Models;

namespace Learning
{
    public class EntitySessionBuilder : ISessionBuilder
    {
        private readonly IMappingRepository _repository;
        private int _sourceSystemId;
        private int _destinationSystemId;
        private UntrainedData _untrainedData;

        public EntitySessionBuilder(IMappingRepository repository)
        {
            _repository = repository;
        }


        public LearningSession GetResult()
        {
            
            var result = new LearningSession(_untrainedData);
            return result;
        }

        public void SetSourceId(int sourceId)
        {
            _sourceSystemId = sourceId;
        }

        public void SetDestinationId(int destinationId)
        {
            _destinationSystemId = destinationId;
        }


        public void BuildUntrainedData()
        {
            _untrainedData = _repository.GetUntrainedData(_sourceSystemId, _destinationSystemId);
        }
    }
}