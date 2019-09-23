using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSustemUIU.Models {
    public class Customer {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Other Contact")]
        public string OtherContact { get; set; }

        [Required]
        [Display(Name = "Customer Address")]
        public string Address { get; set; }

        [Display(Name = "Note")]
        public string Description { get; set; }

        [Display(Name = "Customer Image")]
        public string Image { get; set; }

        [Display(Name = "Customer Area")]
        [NotMapped]
        public string CustomerAreaId { get; set; }
        public Area Area { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Others")]
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}