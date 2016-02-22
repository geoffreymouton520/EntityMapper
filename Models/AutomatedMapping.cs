using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AutomatedMapping
    {
        public LearningResult LearningResult { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
    }
}
