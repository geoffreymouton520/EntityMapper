using System.Collections.Generic;

namespace Learning
{
    public interface IContains
    {
        bool Contains(IEnumerable<string> source, string value);
    }
}