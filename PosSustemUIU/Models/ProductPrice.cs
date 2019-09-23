using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSustemUIU.Models
{
    public class ProductPrice
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string ProductId { get; set; }
        public double Price { get; set; }
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