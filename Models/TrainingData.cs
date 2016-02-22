using System.Collections;
using System.Collections.Generic;

namespace Data.Models
{
    public class TrainingData : IEnumerable<Mapping>
    {
        private readonly IEnumerable<Mapping> _mappings;

        public TrainingData(IEnumerable<Mapping> mappings)
        {
            _mappings = mappings;
        }

        public IEnumerator<Mapping> GetEnumerator()
        {
            return _mappings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_mappings).GetEnumerator();
        }
    }
}