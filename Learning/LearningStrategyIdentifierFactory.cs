using System.Collections.Generic;

namespace Learning
{
    public class LearningStrategyIdentifierFactory : ILearningStrategyIdentifierFactory
    {
        private readonly List<ILearningStrategy> _strategies;

        public LearningStrategyIdentifierFactory()
        {
            _strategies = new List<ILearningStrategy>();
        }

        public LearningStrategyIdentifier CreateInstance()
        {
            return new LearningStrategyIdentifier(_strategies);
        }

        public ILearningStrategy AddStrategy(ILearningStrategy strategy)
        {
            _strategies.Add(strategy);
            return strategy;
        }

        public bool RemoveStrategy(ILearningStrategy strategy)
        {
            return _strategies.Remove(strategy);
        }
    }
}