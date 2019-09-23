using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PosSustemUIU.Models {
    public class Supplier {

        [Key]
        public string Id { get; set; }

        [Required]
        [Display (Name = "Supplier Name")]
        public string Name { get; set; }

        [Display (Name = "Supplier Description")]
        public string Description { get; set; }

        [Display (Name = "Supplier Code")]
        public string Code { get; set; }

        [Display (Name = "Main Contact")]
        public string MainContact { get; set; }

        [Display (Name = "Other Contact")]
        public string OtherContact { get; set; }

        [Display (Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display (Name = "Image")]
        public string Image { get; set; }

        [Display (Name = "Active")]
        public bool IsActive { get; set; }

        [Display (Name = "Others")]
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}