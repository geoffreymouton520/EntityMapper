using System.Collections;
using System.Collections.Generic;

namespace Data.Models
{
    public class LearningResult : IEnumerable<LearntMapping>
    {
        private readonly List<LearntMapping> _learnedMappings;

        public LearningResult(List<LearntMapping> learnedMappings)
        {
            _learnedMappings = learnedMappings;
        }

        public LearntMapping AddLearnedMapping(LearntMapping learntMapping)
        {
            _learnedMappings.Add(learntMapping);
            return learntMapping;
        }

        public IEnumerator<LearntMapping> GetEnumerator()
        {
            return _learnedMappings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_learnedMappings).GetEnumerator();
        }
    }
}