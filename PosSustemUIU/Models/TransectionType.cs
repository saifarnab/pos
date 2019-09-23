using System;
using System.ComponentModel.DataAnnotations;

namespace PosSustemUIU.Models {
    public class TransectionType {
        [Key]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Type Name")]
        public string Name { get; set; }
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Other Note")]
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}