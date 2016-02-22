using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class AutomatedEntityMappingViewModel
    {
        public int DomainId { get; set; }
        public int SourceSystemId { get; set; }
        public int DestinationSystemId { get; set; }
    }
}
