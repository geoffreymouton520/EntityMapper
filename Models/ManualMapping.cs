using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ManualMapping
    {
        public MappingOriginEnum Origin { get {return MappingOriginEnum.Manual;} }
        public List<Mapping> Mappings { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
    }
}
