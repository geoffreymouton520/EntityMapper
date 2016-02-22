using System;

namespace Data.Models
{
    public interface IModifiable
    {
        string ModifiedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}