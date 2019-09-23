using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PosSustemUIU.Models {
    public class Brand {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display (Name = "Brand Name")]
        public string Name { get; set; }

        [Display (Name = "Brand Description")]
        public string Description { get; set; }

        [Display (Name = "Brand Code")]
        public string Code { get; set; }

        [Display (Name = "Brand Image")]
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