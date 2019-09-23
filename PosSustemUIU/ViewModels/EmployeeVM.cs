using System;
using System.ComponentModel.DataAnnotations;

namespace PosSustemUIU.ViewModels
{
    public class EmployeeVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        // public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string OtherContact { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string Meta { get; set; }
    }
}