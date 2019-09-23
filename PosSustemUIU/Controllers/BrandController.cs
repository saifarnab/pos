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
    public class BrandController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public BrandController(ApplicationDbContext context, IHostingEnvironment environment):base()
        {
            _context = context;
            _environment = environment;
        }

        // GET: Brand
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // GET: Brand/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Code,Image,IsActive")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                //upload image 
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    var fileNames = UploadFiles(_environment, files, "brands");
                    if (fileNames.Count > 0)
                    {
                        brand.Image = fileNames[0];
                    }
                }

                //save 
                brand.CreatedBy = GteUserId();
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brand/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,Code,Image,IsActive")] Brand brand)
        {
            if (id != brand.Id)
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
                        var fileNames = UploadFiles(_environment, files, "brands");
                        if (fileNames.Count > 0)
                        {
                            brand.Image = fileNames[0];
                        }
                    }
                    //update
                    brand.UpdatedBy = GteUserId();
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            return View(brand);
        }

        // GET: Brand/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var brand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(string id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }

        public override async Task<IActionResult> ChangeActiveStatus(string id)
        {
            if (!BrandExists(id))
            {
                return NotFound();
            }
        
            var brand = await GetBrandById(id);
            brand.IsActive = !brand.IsActive;
            brand.UpdatedBy = GteUserId();
            _context.Update(brand);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> SoftDelete(string id)
        {
            if (!BrandExists(id))
            {
                return NotFound();
            }
        
            var brand = await GetBrandById(id);
            brand.IsDeleted = !brand.IsDeleted;
            brand.UpdatedBy = GteUserId();
            brand.DeletedBy = GteUserId();
            brand.DeletedAt = DateTime.Now;
            _context.Update(brand);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> Restore(string id)
        {
            if (!BrandExists(id))
            {
                return NotFound();
            }
        
            var brand = await GetBrandById(id);
            brand.IsDeleted = !brand.IsDeleted;
            brand.UpdatedBy = GteUserId();
            _context.Update(brand);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        
        private async Task<Brand> GetBrandById(string id)
        {
            var brand = await _context.Brands.FindAsync(id);
            return brand;
        }
    }
}
