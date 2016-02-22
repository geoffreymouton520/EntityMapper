using System.Collections.Generic;

namespace Data.Models
{
    public class MappingOrigin:INameIdPair
    {
        public MappingOrigin()
        {
            EntityMappings = new HashSet<EntityMapping>();
            PropertyMappings = new HashSet<PropertyMapping>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EntityMapping> EntityMappings { get; set; }
        public virtual ICollection<PropertyMapping> PropertyMappings { get; set; }
    }

    public enum MappingOriginEnum
    {
        Manual,
        Automated
    }
}
