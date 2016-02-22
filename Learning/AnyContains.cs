using System.Collections.Generic;
using System.Linq;

namespace Learning
{
    public class AnyContains : IContains {
        public bool Contains(IEnumerable<string> source, string value)
        {
            return source.Any(t => t.Equals(value));
        }
    }
}