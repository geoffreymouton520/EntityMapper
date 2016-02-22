using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Entity:INameIdPair,ITransactional,IActivatable
    {
        public Entity()
        {
            Properties = new HashSet<Property>();
            SourceMappings = new HashSet<EntityMapping>();
            DestinationMappings = new HashSet<EntityMapping>();
        }
        public int SystemId { get; set; }
        public virtual System System { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Property> Properties { get; }
        public virtual ICollection<EntityMapping> SourceMappings { get; }
        public virtual ICollection<EntityMapping> DestinationMappings { get; }
    }
}
