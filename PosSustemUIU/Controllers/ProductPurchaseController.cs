using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PosSustemUIU.Data;
using PosSustemUIU.Models;
using PosSustemUIU.ViewModels;

namespace PosSustemUIU.Controllers
{
    public class ProductPurchaseController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;

        public ProductPurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductPurchase
        public async Task<IActionResult> Index()
        {
            var productPurchases = _context.ProductPurchases.Include(p => p.Supplier).Include(p => p.TransectionType).OrderByDescending(p =>p.PurchaseDate);
            return View(await productPurchases.ToListAsync());
        }

        // GET: ProductPurchase/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPurchase = await _context.ProductPurchases
                .Include(p => p.Supplier)
                .Include(p => p.TransectionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPurchase == null)
            {
                return NotFound();
            }

            return View(productPurchase);
        }

        // GET: ProductPurchase/Create
        public IActionResult Create()
        {
            ViewBag.Suppliers = _context.Suppliers;
            // ViewBag.Products = _context.Products;
            return View();
        }

        // POST: ProductPurchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PurchaseVM productPurchase)
        {
            // var test = HttpContext.Request.Form["products"];
            // if (ModelState.IsValid)
            // {
            //     _context.Add(productPurchase);
            //     await _context.SaveChangesAsync();
            //     return RedirectToAction(nameof(Index));
            // }
            // ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", productPurchase.SupplierId);
            // ViewData["TransectionTypeOId"] = new SelectList(_context.TransectionType, "Id", "Id", productPurchase.TransectionTypeOId);
            // return View(productPurchase);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductPurchase/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPurchase = await _context.ProductPurchases.FindAsync(id);
            if (productPurchase == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", productPurchase.SupplierId);
            ViewData["TransectionTypeOId"] = new SelectList(_context.TransectionType, "Id", "Id", productPurchase.TransectionTypeOId);
            return View(productPurchase);
        }

        // POST: ProductPurchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ReferenceInternal,ReferenceExternal,PurchaseDate,TotalPrice,TotalVat,TotalQuantity,Note,IsVatPaid,ReceivingCost,IsActive,Meta,IsDeleted,CreatedBy,UpdatedBy,DeletedBy,CreatedAt,DeletedAt,SupplierId,TransectionTypeOId")] ProductPurchase productPurchase)
        {
            if (id != productPurchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPurchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPurchaseExists(productPurchase.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", productPurchase.SupplierId);
            ViewData["TransectionTypeOId"] = new SelectList(_context.TransectionType, "Id", "Id", productPurchase.TransectionTypeOId);
            return View(productPurchase);
        }

        // GET: ProductPurchase/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPurchase = await _context.ProductPurchases
                .Include(p => p.Supplier)
                .Include(p => p.TransectionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPurchase == null)
            {
                return NotFound();
            }

            return View(productPurchase);
        }

        // POST: ProductPurchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productPurchase = await _context.ProductPurchases.FindAsync(id);
            _context.ProductPurchases.Remove(productPurchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPurchaseExists(string id)
        {
            return _context.ProductPurchases.Any(e => e.Id == id);
        }

        [HttpGet("ajax-products")]
        public async Task<JsonResult> GetProductById(){
            var products = await _context.Products.ToListAsync();
            return new JsonResult(products);
        }

        [HttpPost("ajax-save-product-purchase")]
        public async Task<JsonResult> SaveProductsAsync([FromBody]PurchasePostVM purchasePostVM)
        {

            var transectionTypeId = "bda54eb3-c4ea-4a52-a488-9fdaf2bb6e8d";
            var productPurchase = new ProductPurchase{
                SupplierId = purchasePostVM.SupplierId,
                PurchaseDate = Convert.ToDateTime(purchasePostVM.PurchaseDate),
                ReferenceInternal = purchasePostVM.InternalMemo,
                ReferenceExternal = purchasePostVM.ExternalMemo,
                DeliveryNote = purchasePostVM.DeliveryNote,
                PurchaseNote = purchasePostVM.PurchaseNote,
                Note = purchasePostVM.OtherNote,
                ReceivingCost = double.Parse(purchasePostVM.PaidAmount),
                TotalPrice =double.Parse(purchasePostVM.TotalPrice),
                TotalVat =0, //TODO:: Change total vat dynamically
                TotalQuantity=int.Parse(purchasePostVM.TotalQuantity),
                IsVatPaid = purchasePostVM.IsVatPaid,
                IsActive = purchasePostVM.IsActive,
                CreatedBy = GteUserId(),
                TransectionTypeOId =transectionTypeId,
            };

            _context.Add(productPurchase);

            //save products info
            foreach (var product in purchasePostVM.SelectedProducts)
            {
                _context.Add(new Transection{
                    ParentId = productPurchase.Id,
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
           
            return new JsonResult(productPurchase);
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
