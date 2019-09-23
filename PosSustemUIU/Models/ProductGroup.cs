using System;
using System.ComponentModel.DataAnnotations;

namespace PosSustemUIU.Models
{
    public class ProductGroup
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Group Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Group Code")]
        public string Code { get; set; }
        [Display(Name = "Group Description")]
        public string Description { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Others")]
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}