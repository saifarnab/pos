using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSustemUIU.Models
{
    public class Transection
    {
        
        [Key]
        public string Id { get; set; }
        public string ParentId { get; set; }
        [ForeignKey("TransectionTypeId")]
        public TransectionType TransectionType { get; set; }
        public string TransectionTypeId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string ProductId { get; set; }
        public double Price { get; set; }
        public double Vat { get; set; }
        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }
        public DateTime ExpireDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}