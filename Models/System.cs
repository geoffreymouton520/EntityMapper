using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class System:INameIdPair,ITransactional,IActivatable
    {
        public System()
        {
            Entities = new HashSet<Entity>();
        }
        public int DomainId { get; set; }
        public virtual Domain Domain { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Entity> Entities { get; }
    }
}
