using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSustemUIU.Models {
    public class ProductPurchase {
        [Key]
        public string Id { get; set; }
        public string ReferenceInternal { get; set; }
        public string ReferenceExternal { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double TotalPrice { get; set; }
        public double TotalVat { get; set; }
        public int TotalQuantity { get; set; }
        public string Note { get; set; }
        public bool IsVatPaid { get; set; }
        public double ReceivingCost { get; set; }
        public string PurchaseNote { get; set; }
        public string DeliveryNote { get; set; }
        public string Attachment { get; set; }
        public bool IsActive { get; set; }
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Supplier Supplier { get; set; }
        [Required]
        [ForeignKey("Supplier")]
        public string SupplierId { get; set; }

        public TransectionType TransectionType { get; set; }
        [Required]
        [ForeignKey("TransectionType")]
        public string TransectionTypeOId { get; set; }
    }
}