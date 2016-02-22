namespace Data.Models
{
    public interface IMapping<TMappingClass> where TMappingClass:class 
    {
        int SourceId { get; set; }
        int DestinationId { get; set; }
        TMappingClass Source { get; set; }
        TMappingClass Destination { get; set; }
    }
}