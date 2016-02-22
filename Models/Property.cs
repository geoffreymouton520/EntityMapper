using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Property:INameIdPair,ITransactional,IActivatable
    {
        public Property()
        {
            SourceMappings = new HashSet<PropertyMapping>();
            DestinationMappings = new HashSet<PropertyMapping>();
        }
        public int EntityId { get; set; }
        public virtual Entity Entity { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<PropertyMapping> SourceMappings { get; }
        public virtual ICollection<PropertyMapping> DestinationMappings { get; }
    }
}
