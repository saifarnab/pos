using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PosSustemUIU.Models;

namespace PosSustemUIU.ViewModels
{
    public class ProductVM
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Product Name Required")]
        [Display(Name = "Product Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "Product Display")]
        public string Description { get; set; }
        [Display(Name = "Product Image")]
        public string Image { get; set; }
        [Display(Name = "Searcable Keywords")]
        public string KeyWord { get; set; }
        [Display(Name = "Product Code")]
        public string Code { get; set; }
        [Display(Name = "Product Expire Date")]
        [Required]
        public DateTime ExpireDate { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Other Note")]
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [Display(Name = "Product Category")]
        public string ProductCategoryID { get; set; }
        [Required]
        [Display(Name = "Product Supplier")]
        public string SupplierId { get; set; }
        [Required]
        [Display(Name = "Product Brand")]
        public string BrandId { get; set; }
        [Display(Name = "Product Group")]
        public string ProductGroupID { get; set; }
        [Display(Name = "Product Barcode")]
        [Required]
        public string Barcode { get; set; }
        [Display(Name = "Product Unit")]
        [Required]
        public string  UnitId { get; set; }
        [Display(Name = "Product Price")]
        [Required]
        public double Price { get; set; }
        [Display(Name = "Product Vat")]
        public double Vat { get; set; }
        [Display(Name = "Product Discount")]
        public double Discount { get; set; }
        

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public IEnumerable<ProductCategory> Categories { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<ProductGroup> ProductGroups { get; set; }
        public IEnumerable<UnitType> UnitTypes { get; set; }

        public ProductBarcode ProductBarcode { get; set; }
        public ProductPrice ProductPrice { get; set; }
        public ProductDiscount ProductDiscount { get; set; }
        public ProductVat ProductVat { get; set; }
        public ProductUnit ProductUnit { get; set; }

    }
}