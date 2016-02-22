using System.Collections.Generic;

namespace Data.Models
{
    public class UntrainedData
    {
        public IEnumerable<string> Sources { get; set; }
        public IEnumerable<string> Destinations { get; set; }
    }
}