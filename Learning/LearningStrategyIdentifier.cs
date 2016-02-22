using System.Collections.Generic;
using System.Linq;
using Data.Models;

namespace Learning
{
    public class LearningStrategyIdentifier : ILearningStrategyIdentifier
    {
        private readonly List<ILearningStrategy> _strategies;


        public LearningStrategyIdentifier(List<ILearningStrategy> strategies)
        {
            _strategies = strategies;
        }

        public ILearningStrategy Identify(Mapping mapping)
        {
            return _strategies.FirstOrDefault(strategy => strategy.Apply(mapping.Source).Equals(mapping.Destination));
        }

        public ILearningStrategy AddStrategy(ILearningStrategy strategy)
        {
            _strategies.Add(strategy);
            return strategy;
        }
    }
}