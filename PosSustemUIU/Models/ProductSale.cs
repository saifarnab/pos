using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSustemUIU.Models {
    public class ProductSale {
        [Key]
        public string Id { get; set; }
        public string ReferenceInternal { get; set; }
        public string ReferenceExternal { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime PostingDate { get; set; }
        public double TotalPrice { get; set; }
        public double TotalVat { get; set; }
        public double TotalDiscount { get; set; }
        public int TotalQuantity { get; set; }
        public string Note { get; set; }
        public string PaymentNote { get; set; }
        public bool IsVatPaid { get; set; }
        public double ReceivingCost { get; set; }
        public bool IsActive { get; set; }
        public string Meta { get; set; }
        public string CustomerMeta { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Customer Customer { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        public TransectionType TransectionType { get; set; }
        [Required]
        [ForeignKey("TransectionType")]
        public string TransectionTypeOId { get; set; }

    }
}