using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PosSustemUIU.Models;

namespace PosSustemUIU.ViewModels
{
    public class PurchaseVM
    {
        public string Id { get; set; }
        [Display(Name = "Supplier")]
        [Required]
        public string SupplierId { get; set; }
        [Display(Name = "Internal Memo")]
        public string ReferenceInternal { get; set; }
        [Display(Name = "External Memo")]
        public string ReferenceExternal { get; set; }
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }
        [Display(Name = "Purchase Note")]
        public string PurchaseNote { get; set; }
        [Display(Name = "Delivery Note")]
        public string DeliveryNote { get; set; }
        [Display(Name = "Attachment")]
        public string Attachment { get; set; }
        [Display(Name = "Paid Amount")]
        public double ReceivingCost { get; set; }
        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }
        [Display(Name = "Total Vat")]
        public double TotalVat { get; set; }
        [Display(Name = "Total Quantity")]
        public int TotalQuantity { get; set; }
        [Display(Name = "Vat Paid")]
        public bool IsVatPaid { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Other Note")]
        public string Meta { get; set; }

        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
        [JsonIgnore]
        public IEnumerable<Area> Areas { get; set; }

    }
}