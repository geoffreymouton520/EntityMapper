using System;

namespace Data.Models
{
    public interface ICreatable
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
    }
}