using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PosSustemUIU.Models {
    public class Area {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display (Name = "Area Name")]
        public string Name { get; set; }
        [Display (Name = "Description")]
        public string Description { get; set; }

        [Display (Name = "Text Code")]
        public string TextCode { get; set; }

        [Display (Name = "Numeric Code")]
        public string NumericCode { get; set; }

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

        public ICollection<Customer> Customers { get; set; }

    }
}