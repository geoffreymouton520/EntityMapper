namespace Learning
{
    public class LearningSessionDirector
    {
        public void Constructor(ISessionBuilder builder, int sourceId, int destinationId)
        {
            builder.SetSourceId(sourceId);
            builder.SetDestinationId(destinationId);
            builder.BuildUntrainedData();
        }
    }
}