using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class DomainViewModel: IActivatableViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Domain Name is required.")]
        [MinLength(4, ErrorMessage = "Domain name is too short.")]
        [MaxLength(50,ErrorMessage = "Domain name is too long.")]
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [Required]
        public bool Active { get; set; }
        public string Status { get; set; }
        public string Label { get; set; }
    }
}
