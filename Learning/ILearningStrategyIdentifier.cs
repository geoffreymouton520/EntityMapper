using Data.Models;

namespace Learning
{
    public interface ILearningStrategyIdentifier
    {
        ILearningStrategy Identify(Mapping mapping);
        ILearningStrategy AddStrategy(ILearningStrategy strategy);
    }
}