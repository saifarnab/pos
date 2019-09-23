using System;
using System.ComponentModel.DataAnnotations;

namespace PosSustemUIU.Models
{
    public class StoreConfiguration
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Store Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Store Sologan")]
        [Required]
        public string Sologan { get; set; }
        [Display(Name = "Store Address")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "Store Logo")]
        public string Logo { get; set; }
        [Display(Name = "Contact Number")]
        [Required]
        public string MainContact { get; set; }
        [Display(Name = "Other Contact Number")]
        public string OtherContact { get; set; }
        [Display(Name = "Store Email")]
        public string Email { get; set; }
        [Display(Name = "Store Website")]
        public string Website { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "other Note")]
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}