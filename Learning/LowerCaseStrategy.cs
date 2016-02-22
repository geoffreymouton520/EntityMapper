namespace Learning
{
    public class LowerCaseStrategy : ILearningStrategy
    {
        public string Apply(string source)
        {
            return source.ToLower();
        }
    }
}