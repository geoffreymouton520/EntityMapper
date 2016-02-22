namespace Data.Models
{
    public interface IOrigin
    {
        int MappingOriginId { get; set; }
        MappingOrigin MappingOrigin { get; set; }
    }
}