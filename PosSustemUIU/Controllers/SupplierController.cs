using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PosSustemUIU.Data;
using PosSustemUIU.Models;

namespace PosSustemUIU.Controllers
{
    public class SupplierController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public SupplierController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suppliers.ToListAsync());
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Code,MainContact,OtherContact,Email,Image,IsActive")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //upload image 
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    var fileNames = UploadFiles(_environment, files, "suppliers");
                    if (fileNames.Count > 0)
                    {
                        supplier.Image = fileNames[0];
                    }
                }

                //save 
                supplier.CreatedBy = GteUserId();
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,Code,MainContact,OtherContact,Email,Image,IsActive")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //upload image 
                    var files = HttpContext.Request.Form.Files;
                    if (files != null)
                    {
                        var fileNames = UploadFiles(_environment, files, "suppliers");
                        if (fileNames.Count > 0)
                        {
                            supplier.Image = fileNames[0];
                        }
                    }
                    //update
                    supplier.UpdatedBy = GteUserId();
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
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
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(string id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }

        public override async Task<IActionResult> ChangeActiveStatus(string id)
        {
            if (!SupplierExists(id))
            {
                return NotFound();
            }
        
            var supplier = await GetSuplierById(id);
            supplier.IsActive = !supplier.IsActive;
            supplier.UpdatedBy = GteUserId();
            _context.Update(supplier);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> SoftDelete(string id)
        {
            if (!SupplierExists(id))
            {
                return NotFound();
            }
        
            var supplier = await GetSuplierById(id);
            supplier.IsDeleted = !supplier.IsDeleted;
            supplier.UpdatedBy = GteUserId();
            supplier.DeletedBy = GteUserId();
            supplier.DeletedAt = DateTime.Now;
            _context.Update(supplier);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> Restore(string id)
        {
            if (!SupplierExists(id))
            {
                return NotFound();
            }
        
            var supplier = await GetSuplierById(id);
            supplier.IsDeleted = !supplier.IsDeleted;
            supplier.UpdatedBy = GteUserId();
            _context.Update(supplier);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        
        private async Task<Supplier> GetSuplierById(string id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            return supplier;
        }
    }
}
