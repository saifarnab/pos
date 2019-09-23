using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSustemUIU.Models {
    public class Product {
        [Key]
        public string Id { get; set; }

        [Required (ErrorMessage = "Product Name Required")]
        [MaxLength (50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string KeyWord { get; set; }
        public string Code { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }
        public string Meta { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("ProductCategoryID")]
        public ProductCategory ProductCategory { get; set; }
        [Required]
        public string ProductCategoryID { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        [Required]
        public string SupplierId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        [Required]
        public string BrandId { get; set; }
        [ForeignKey("ProductGroupID")]
        public ProductGroup ProductGroup { get; set; }
        public string ProductGroupID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public ICollection<ProductUnit> ProductUnits { get; set; }
        public ICollection<ProductBarcode> ProductBarcodes { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }
        public ICollection<ProductVat> ProductVats { get; set; }
        public ICollection<ProductDiscount> ProductDiscounts { get; set; }


    }
}