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
    public class UnitTypeController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;

        public UnitTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnitType
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitTypes.ToListAsync());
        }

        // GET: UnitType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _context.UnitTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitType == null)
            {
                return NotFound();
            }

            return View(unitType);
        }

        // GET: UnitType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Code,IsActive,Meta")] UnitType unitType)
        {
            if (ModelState.IsValid)
            {
                unitType.CreatedBy = GteUserId();
                _context.Add(unitType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitType);
        }

        // GET: UnitType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _context.UnitTypes.FindAsync(id);
            if (unitType == null)
            {
                return NotFound();
            }
            return View(unitType);
        }

        // POST: UnitType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,Code,IsActive,Meta")] UnitType unitType)
        {
            if (id != unitType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitType.UpdatedBy = GteUserId();
                    _context.Update(unitType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitTypeExists(unitType.Id))
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
            return View(unitType);
        }

        // GET: UnitType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _context.UnitTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitType == null)
            {
                return NotFound();
            }

            return View(unitType);
        }

        // POST: UnitType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var unitType = await _context.UnitTypes.FindAsync(id);
            _context.UnitTypes.Remove(unitType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitTypeExists(string id)
        {
            return _context.UnitTypes.Any(e => e.Id == id);
        }

        public override async Task<IActionResult> ChangeActiveStatus(string id)
        {
            if (!UnitTypeExists(id))
            {
                return NotFound();
            }
        
            var unitType = await GetUnitTypeById(id);
            unitType.IsActive = !unitType.IsActive;
            unitType.UpdatedBy = GteUserId();
            _context.Update(unitType);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> SoftDelete(string id)
        {
            if (!UnitTypeExists(id))
            {
                return NotFound();
            }
        
            var unitType = await GetUnitTypeById(id);
            unitType.IsDeleted = !unitType.IsDeleted;
            unitType.UpdatedBy = GteUserId();
            unitType.DeletedBy = GteUserId();
            unitType.DeletedAt = DateTime.Now;
            _context.Update(unitType);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> Restore(string id)
        {
            if (!UnitTypeExists(id))
            {
                return NotFound();
            }
        
            var unitType = await GetUnitTypeById(id);
            unitType.IsDeleted = !unitType.IsDeleted;
            unitType.UpdatedBy = GteUserId();
            _context.Update(unitType);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        
        private async Task<UnitType> GetUnitTypeById(string id)
        {
            var unitType = await _context.UnitTypes.FindAsync(id);
            return unitType;
        }
    }
}
