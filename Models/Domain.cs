using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Domain : ITransactional, IActivatable, INameIdPair
    {
        public Domain()
        {
            Systems = new HashSet<System>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<System> Systems { get; }

    }
}
