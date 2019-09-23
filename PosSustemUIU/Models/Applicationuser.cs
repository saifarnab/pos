using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PosSustemUIU.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherContact { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Start { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}