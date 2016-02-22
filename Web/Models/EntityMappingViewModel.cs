using System;

namespace Web.Models
{
    public class EntityMappingViewModel
    {
        public int DomainId { get; set; }
        public int SourceSystemId { get; set; }
        public int DestinationSystemId { get; set; }
        public int SourceEntityId { get; set; }
        public int DestinationEntityId { get; set; }
        public string SourceEntity { get; set; }
        public string DestinationEntity { get; set; }
        public int MappingOriginId { get; set; }
        public string MappingOrigin { get; set; }
        public bool Confirmed { get; set; }
        public bool? Correct { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public int Id { get; set; }
        public string ConfirmedStatus { get; set; }
        public string ConfirmedLabel { get; set; }
        public string CorrectStatus { get; set; }
        public string CorrectLabel { get; set; }
    }
}
