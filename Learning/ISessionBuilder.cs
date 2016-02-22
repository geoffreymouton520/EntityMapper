namespace Learning
{
    public interface ISessionBuilder
    {
        LearningSession GetResult();
        void SetSourceId(int sourceId);
        void SetDestinationId(int destinationId);
        void BuildUntrainedData();
    }
}