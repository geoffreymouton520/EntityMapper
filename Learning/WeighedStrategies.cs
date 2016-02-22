using System.Collections.Generic;
using System.Linq;

namespace Learning
{
    public class WeighedStrategies
    {
        private Dictionary<ILearningStrategy, int> _weightedStrategies;

        public WeighedStrategies()
        {
            _weightedStrategies = new Dictionary<ILearningStrategy, int>();
        }

        public int Add(ILearningStrategy strategy)
        {
            if (_weightedStrategies.ContainsKey(strategy))
            {
                _weightedStrategies[strategy]++;
            }
            else
            {
                _weightedStrategies.Add(strategy, 1);
            }
            return _weightedStrategies[strategy];
        }

        public IEnumerable<ILearningStrategy> Strategies
        {
            get
            {
                return _weightedStrategies.OrderBy(t => t.Value).Select(t => t.Key);
            }
        }
    }
}