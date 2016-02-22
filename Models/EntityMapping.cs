using System;

namespace Data.Models
{
    public class EntityMapping:IConfirmable,ICorrectable,ICreatable,IReviewable, IMapping<Entity>,IEntity,IOrigin
    {
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public virtual Entity Source { get; set; }
        public virtual Entity Destination { get; set; }
        public bool Confirmed { get; set; }
        public bool? Correct { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public int Id { get; set; }
        public int MappingOriginId { get; set; }
        public virtual MappingOrigin MappingOrigin { get; set; }
    }
}
