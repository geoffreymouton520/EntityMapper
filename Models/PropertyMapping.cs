using System;

namespace Data.Models
{
    public class PropertyMapping:IConfirmable,ICorrectable,ICreatable,IReviewable,IMapping<Property>,IEntity,IOrigin
    {
        public bool Confirmed { get; set; }
        public bool? Correct { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public Property Source { get; set; }
        public Property Destination { get; set; }
        public int Id { get; set; }
        public int MappingOriginId { get; set; }
        public virtual MappingOrigin MappingOrigin { get; set; }
    }
}