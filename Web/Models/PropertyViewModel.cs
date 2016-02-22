using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PropertyViewModel:IActivatableViewModel
    {
        public int EntityId { get; set; }
        public string Entity { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public string Label { get; set; }
    }
}
