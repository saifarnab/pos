using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PosSustemUIU.Data;
using PosSustemUIU.Models;
using PosSustemUIU.ViewModels;

namespace PosSustemUIU.Controllers
{
    // [Authorize(Roles = "Administrator, Employee")]
    public class ProductSaleController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;

        public ProductSaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductSale
        public async Task<IActionResult> Index()
        {
            var productSales = _context.ProductSales.Include(p => p.Customer).Include(p => p.TransectionType);
            return View(await productSales.ToListAsync());
        }

        // GET: ProductSale/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSale = await _context.ProductSales
                .Include(p => p.Customer)
                .Include(p => p.TransectionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSale == null)
            {
                return NotFound();
            }

            return View(productSale);
        }

        // GET: ProductSale/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = await _context.Customers.ToListAsync();
            // ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            // ViewData["TransectionTypeOId"] = new SelectList(_context.TransectionType, "Id", "Id");
            return View();
        }

        // POST: ProductSale/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductSale productSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productSale.CustomerId);
            ViewData["TransectionTypeOId"] = new SelectList(_context.TransectionType, "Id", "Id", productSale.TransectionTypeOId);
            return View(productSale);
        }

        // GET: ProductSale/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSale = await _context.ProductSales.FindAsync(id);
            if (productSale == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productSale.CustomerId);
            ViewData["TransectionTypeOId"] = new SelectList(_context.TransectionType, "Id", "Id", productSale.TransectionTypeOId);
            return View(productSale);
        }

        // POST: ProductSale/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProductSale productSale)
        {
            if (id != productSale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSaleExists(productSale.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productSale.CustomerId);
            ViewData["TransectionTypeOId"] = new SelectList(_context.TransectionType, "Id", "Id", productSale.TransectionTypeOId);
            return View(productSale);
        }

        // GET: ProductSale/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSale = await _context.ProductSales
                .Include(p => p.Customer)
                .Include(p => p.TransectionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSale == null)
            {
                return NotFound();
            }

            return View(productSale);
        }

        // POST: ProductSale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productSale = await _context.ProductSales.FindAsync(id);
            _context.ProductSales.Remove(productSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSaleExists(string id)
        {
            return _context.ProductSales.Any(e => e.Id == id);
        }

        [HttpPost("ajax-save-product-sale")]
        public async Task<JsonResult> SaveProductsAsync([FromBody]SalePostVM salePostVM)
        {

            var transectionTypeId = "9d87f732-8a45-478b-a725-1d566974f947";
            var productSale = new ProductSale
            {
                CustomerId = salePostVM.CustomerId,
                SaleDate = Convert.ToDateTime(salePostVM.SaleDate),
                ReferenceInternal = salePostVM.InternalMemo,
                ReferenceExternal = salePostVM.ExternalMemo,
                Note = salePostVM.SaleNote,
                ReceivingCost = double.Parse(salePostVM.PaidAmount),
                TotalPrice = double.Parse(salePostVM.TotalPrice),
                TotalDiscount = double.Parse(salePostVM.Discount),
                TotalVat = 0, //TODO:: Change total vat dynamically
                TotalQuantity = int.Parse(salePostVM.TotalQuantity),
                IsVatPaid = salePostVM.IsVatPaid,
                IsActive = salePostVM.IsActive,
                CreatedBy = GteUserId(),
                TransectionTypeOId = transectionTypeId,
            };

            _context.Add(productSale);

            //save products info
            foreach (var product in salePostVM.SelectedProducts)
            {
                //update purchase product quantity
                _context.Add(new Transection
                {
                    ParentId = productSale.Id,
                    ProductId = product.ProductId,
                    Price = double.Parse(product.Price),
                    Quantity = int.Parse(product.Quantity),
                    RemainingQuantity = int.Parse(product.Quantity),
                    ExpireDate = Convert.ToDateTime(product.ExpireDate),
                    CreatedBy = GteUserId(),
                    TransectionTypeId = transectionTypeId
                });
            }
            var res = await _context.SaveChangesAsync();

            return new JsonResult(productSale);
        }

        [HttpGet("ajax-sale-products")]
        public async Task<JsonResult> GetProductById()
        {
            var transectionTypeId = "bda54eb3-c4ea-4a52-a488-9fdaf2bb6e8d";

            var transection = await _context.Transections.Where(t => t.TransectionTypeId == transectionTypeId && t.RemainingQuantity > 0).Include("Product").Select(prod => 
                new {TransectionId = prod.Id, prod.Product.Id, prod.Product.Name, prod.Price, prod.RemainingQuantity, prod.ExpireDate}
            ).ToListAsync();

            // var products = await _context.Products.ToListAsync();
            return new JsonResult(transection);
        }

        public override Task<IActionResult> ChangeActiveStatus(string id)
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> SoftDelete(string id)
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> Restore(string id)
        {
            throw new NotImplementedException();
        }
    }
}

