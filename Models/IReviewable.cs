using System;

namespace Data.Models
{
    public interface IReviewable
    {
        string ReviewedBy { get; set; }
        DateTime? ReviewedOn { get; set; }
    }
}