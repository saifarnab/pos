using Microsoft.AspNetCore.Hosting;
using PosSustemUIU.Data;
using PosSustemUIU.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PosSustemUIU.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PosSustemUIU.Models.BLL
{
    public class ProductManager
    {
        private readonly ApplicationDbContext _context;
        private ProductVM _productVM;

        public ProductManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveProductAsync(ProductVM productVM){

            var product = new Product
            {
                Name = productVM.Name,
                Description = productVM.Description,
                Image = productVM.Image,
                KeyWord = productVM.KeyWord,
                Code = productVM.Code,
                ExpireDate = productVM.ExpireDate,
                IsActive = productVM.IsActive,
                Meta = productVM.Meta,
                ProductCategoryID = productVM.ProductCategoryID,
                SupplierId = productVM.SupplierId,
                BrandId = productVM.BrandId,
                ProductGroupID = productVM.ProductGroupID,
                CreatedBy = productVM.CreatedBy,
                CreatedAt = DateTime.Now,
            };

            _context.Add(product);
            //save barcode
            _context.Add(new ProductBarcode{
                Barcode = productVM.Barcode,
                ProductId = product.Id,
                CreatedBy = productVM.CreatedBy,
                IsActive = true,
                CreatedAt = DateTime.Now
            });
            //save vat
            _context.Add(new ProductVat
            {
                Vat = productVM.Vat,
                ProductId = product.Id,
                CreatedBy = productVM.CreatedBy,
                IsActive = true,
                CreatedAt = DateTime.Now
            });
            //save price
            _context.Add(new ProductPrice
            {
                Price = productVM.Price,
                ProductId = product.Id,
                CreatedBy = productVM.CreatedBy,
                IsActive = true,
                CreatedAt = DateTime.Now
            });
            //save discount
            if (productVM.Discount > 0)
            {
                _context.Add(new ProductDiscount
                {
                    Discount = productVM.Discount,
                    ProductId = product.Id,
                    CreatedBy = productVM.CreatedBy,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
            }

            //save unit
            _context.Add(new ProductUnit
            {
                UnitTypeId = productVM.UnitId,
                ProductId = product.Id,
                CreatedBy = productVM.CreatedBy,
                IsActive = true,
                CreatedAt = DateTime.Now
            });

            var res = await _context.SaveChangesAsync();

            if(res > 0)
                return true;
            else
            {
                _productVM = productVM;
                return false;
            }

        }

        public async Task<ProductVM> GetProductVMAsync()
        {
            // var product = new ProductVM();
            if (_productVM == null)
            {
                _productVM = new ProductVM();
            }
            
            _productVM  = await FillWithRelatedDataAsync(_productVM);
            return _productVM;
        }

        public async Task<ProductVM> ProductToProductVMAsync(Product product)
        {
            var productVM = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                KeyWord = product.KeyWord,
                Code = product.Code,
                ExpireDate = product.ExpireDate,
                IsActive = product.IsActive,
                Meta = product.Meta,
                ProductCategoryID = product.ProductCategoryID,
                SupplierId = product.SupplierId,
                BrandId = product.BrandId,
                ProductGroupID = product.ProductGroupID,
                CreatedBy = product.CreatedBy,
                CreatedAt = DateTime.Now,
            };

            return await FillWithRelatedDataAsync(productVM);
            
        }

        public async Task<ProductVM> GetRelatedData(ProductVM productVM)
        {
            //barcode
            var productBarcode = await _context.ProductBarcodes.Where( p => p.ProductId == productVM.Id && p.IsActive == true).FirstOrDefaultAsync();
            if(productBarcode != null)
            productVM.ProductBarcode = productBarcode;
            //discount
            var productDiscount = await _context.ProductDiscounts.Where(p => p.ProductId == productVM.Id && p.IsActive == true).FirstOrDefaultAsync();
            if(productDiscount != null)
            productVM.ProductDiscount = productDiscount;
            //vat
            var productVat = await _context.ProductVats.Where(p => p.ProductId == productVM.Id && p.IsActive == true).FirstOrDefaultAsync();
            if(productVat != null)
            productVM.ProductVat = productVat;
            //price
            var productprice = await _context.ProductPrices.Where(p => p.ProductId == productVM.Id && p.IsActive == true).FirstOrDefaultAsync();
            if(productprice != null)
            productVM.ProductPrice = productprice;
            //unit
            var ProductUnit = await _context.ProductUnits.Where(p => p.ProductId == productVM.Id && p.IsActive == true).FirstOrDefaultAsync();
            if(ProductUnit != null)
            productVM.ProductUnit  = ProductUnit;
            return productVM;
        }

        public async Task<bool> UpdateProductAsync(ProductVM productVM)
        {
            var product = new Product
            {
                Id = productVM.Id,
                Name = productVM.Name,
                Description = productVM.Description,
                Image = productVM.Image,
                KeyWord = productVM.KeyWord,
                Code = productVM.Code,
                ExpireDate = productVM.ExpireDate,
                IsActive = productVM.IsActive,
                Meta = productVM.Meta,
                ProductCategoryID = productVM.ProductCategoryID,
                SupplierId = productVM.SupplierId,
                BrandId = productVM.BrandId,
                ProductGroupID = productVM.ProductGroupID,
                UpdatedBy = productVM.CreatedBy,
                CreatedAt = productVM.CreatedAt,
            };

            _context.Update(product);
           productVM = await GetRelatedData(productVM);
            //save barcode
            if (productVM.Barcode != productVM.ProductBarcode.Barcode)
            {
                productVM.ProductBarcode.IsActive = !productVM.ProductBarcode.IsActive;
                _context.Update(productVM.ProductBarcode);
                _context.Add(new ProductBarcode
                {
                    Barcode = productVM.Barcode,
                    ProductId = product.Id,
                    CreatedBy = productVM.CreatedBy,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
            }

            //save vat
            if (productVM.Vat != productVM.ProductVat.Vat)
            {
                productVM.ProductVat.IsActive = !productVM.ProductVat.IsActive;
                _context.Update(productVM.ProductVat);
                _context.Add(new ProductVat
                {
                    Vat = productVM.Vat,
                    ProductId = product.Id,
                    CreatedBy = productVM.CreatedBy,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
            }
            //save price
            if (productVM.Price != productVM.ProductPrice.Price)
            {
                productVM.ProductPrice.IsActive = !productVM.ProductPrice.IsActive;
                _context.Update(productVM.ProductPrice);
                _context.Add(new ProductPrice
                {
                    Price = productVM.Price,
                    ProductId = product.Id,
                    CreatedBy = productVM.CreatedBy,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
            }
            
            //save discount
            if (productVM.Discount != productVM.ProductDiscount.Discount)
            {
                productVM.ProductDiscount.IsActive = !productVM.ProductDiscount.IsActive;
                _context.Update(productVM.ProductDiscount);

                _context.Add(new ProductDiscount
                {
                    Discount = productVM.Discount,
                    ProductId = product.Id,
                    CreatedBy = productVM.CreatedBy,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
            }
            

            //save unit
            if (productVM.UnitId != productVM.ProductUnit.UnitTypeId)
            {
                productVM.ProductUnit.IsActive = !productVM.ProductUnit.IsActive;
                _context.Update(productVM.ProductUnit);

                _context.Add(new ProductUnit
                {
                    UnitTypeId = productVM.UnitId,
                    ProductId = product.Id,
                    CreatedBy = productVM.CreatedBy,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
            }
            

            var res = await _context.SaveChangesAsync();

            if (res > 0)
                return true;
            else
            {
                _productVM = productVM;
                return false;
            }
        }

        private async Task<ProductVM> FillWithRelatedDataAsync(ProductVM productVM){
            productVM.Categories = await _context.ProductCategories.Where(c => c.IsActive == true).ToListAsync();
            productVM.Suppliers = await _context.Suppliers.Where(s => s.IsActive == true).ToListAsync();
            productVM.Suppliers = await _context.Suppliers.Where(s => s.IsActive == true).ToListAsync();
            productVM.Brands = await _context.Brands.Where(b => b.IsActive == true).ToListAsync();
            productVM.Brands = await _context.Brands.Where(b => b.IsActive == true).ToListAsync();
            productVM.ProductGroups = await _context.ProductGroups.Where(g => g.IsActive == true).ToListAsync();
            productVM.UnitTypes = await _context.UnitTypes.Where(g => g.IsActive == true).ToListAsync();

            return productVM;
        }
    }
}