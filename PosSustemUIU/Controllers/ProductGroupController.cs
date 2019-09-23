using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PosSustemUIU.Data;
using PosSustemUIU.Models;

namespace PosSustemUIU.Controllers
{
    public class ProductGroupController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;

        public ProductGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductGroup
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductGroups.ToListAsync());
        }

        // GET: ProductGroup/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // GET: ProductGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,Description,IsActive,Meta")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                productGroup.CreatedBy = GteUserId();
                _context.Add(productGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productGroup);
        }

        // GET: ProductGroup/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return NotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Code,Description,IsActive,Meta")] ProductGroup productGroup)
        {
            if (id != productGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productGroup.UpdatedBy = GteUserId();
                    _context.Update(productGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGroupExists(productGroup.Id))
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
            return View(productGroup);
        }

        // GET: ProductGroup/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: ProductGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productGroup = await _context.ProductGroups.FindAsync(id);
            _context.ProductGroups.Remove(productGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductGroupExists(string id)
        {
            return _context.ProductGroups.Any(e => e.Id == id);
        }

        public override async Task<IActionResult> ChangeActiveStatus(string id)
        {
            if (!ProductGroupExists(id))
            {
                return NotFound();
            }
        
            var productGroup = await GetGroupById(id);
            productGroup.IsActive = !productGroup.IsActive;
            productGroup.UpdatedBy = GteUserId();
            _context.Update(productGroup);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> SoftDelete(string id)
        {
            if (!ProductGroupExists(id))
            {
                return NotFound();
            }
        
            var productGroup = await GetGroupById(id);
            productGroup.IsDeleted = !productGroup.IsDeleted;
            productGroup.UpdatedBy = GteUserId();
            productGroup.DeletedBy = GteUserId();
            productGroup.DeletedAt = DateTime.Now;
            _context.Update(productGroup);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> Restore(string id)
        {
            if (!ProductGroupExists(id))
            {
                return NotFound();
            }
        
            var productGroup = await GetGroupById(id);
            productGroup.IsDeleted = !productGroup.IsDeleted;
            productGroup.UpdatedBy = GteUserId();
            _context.Update(productGroup);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        
        private async Task<ProductGroup> GetGroupById(string id)
        {
            var productGroup = await _context.ProductGroups.FindAsync(id);
            return productGroup;
        }
    }
}
