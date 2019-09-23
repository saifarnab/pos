using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSustemUIU.Data;
using PosSustemUIU.Models;

namespace PosSustemUIU.Controllers
{
    // [Authorize(Roles = "Administrator")]
    public class AreaController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;

        public AreaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Area
        public async Task<IActionResult> Index()
        {
            return View(await _context.Areas.ToListAsync());
        }

        // GET: Area/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Area/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,TextCode,NumericCode,IsActive")] Area area)
        {
            if (ModelState.IsValid)
            {
                area.CreatedAt = DateTime.Now;
                area.CreatedBy =GteUserId();
                _context.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET: Area/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,TextCode,NumericCode,IsActive")] Area area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    area.UpdatedBy = GteUserId();
                    _context.Update(area);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.Id))
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
            return View(area);
        }

        // GET: Area/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var area = await _context.Areas.FindAsync(id);
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(string id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }

        public override async Task<IActionResult> ChangeActiveStatus(string id)
        {
            if (!AreaExists(id))
            {
                return NotFound();
            }

            var area = await GetAreabyId(id);
            area.IsActive = !area.IsActive;
            area.UpdatedBy = GteUserId();
            _context.Update(area);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public override async Task<IActionResult> SoftDelete(string id)
        {
            if (!AreaExists(id))
            {
                return NotFound();
            }

            var area = await GetAreabyId(id);
            area.IsDeleted = !area.IsDeleted;
            area.UpdatedBy = GteUserId();
            area.DeletedBy = GteUserId();
            area.DeletedAt = DateTime.Now;
            _context.Update(area);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public override async Task<IActionResult> Restore(string id)
        {
            if (!AreaExists(id))
            {
                return NotFound();
            }

            var area = await GetAreabyId(id);
            area.IsDeleted = !area.IsDeleted;
            area.UpdatedBy = GteUserId();
            _context.Update(area);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private async Task<Area> GetAreabyId(string id)
        {
            var area = await _context.Areas.FindAsync(id);
            return area;
        }

    }
}
