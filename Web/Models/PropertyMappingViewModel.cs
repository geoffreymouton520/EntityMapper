using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PropertyMappingViewModel
    {
        public bool Confirmed { get; set; }
        public bool Correct { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Id { get; set; }
    }
}
