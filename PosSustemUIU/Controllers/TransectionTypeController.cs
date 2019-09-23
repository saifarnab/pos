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
    public class TransectionTypeController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;

        public TransectionTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransectionType
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransectionType.ToListAsync());
        }

        // GET: TransectionType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transectionType = await _context.TransectionType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transectionType == null)
            {
                return NotFound();
            }

            return View(transectionType);
        }

        // GET: TransectionType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransectionType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,GroupName,IsActive,Meta")] TransectionType transectionType)
        {
            if (ModelState.IsValid)
            {
                transectionType.CreatedBy = GteUserId();
                _context.Add(transectionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transectionType);
        }

        // GET: TransectionType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transectionType = await _context.TransectionType.FindAsync(id);
            if (transectionType == null)
            {
                return NotFound();
            }
            return View(transectionType);
        }

        // POST: TransectionType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,GroupName,IsActive,Meta")] TransectionType transectionType)
        {
            if (id != transectionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    transectionType.UpdatedBy = GteUserId();
                    _context.Update(transectionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransectionTypeExists(transectionType.Id))
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
            return View(transectionType);
        }

        // GET: TransectionType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transectionType = await _context.TransectionType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transectionType == null)
            {
                return NotFound();
            }

            return View(transectionType);
        }

        // POST: TransectionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transectionType = await _context.TransectionType.FindAsync(id);
            _context.TransectionType.Remove(transectionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransectionTypeExists(string id)
        {
            return _context.TransectionType.Any(e => e.Id == id);
        }

        public override async Task<IActionResult> ChangeActiveStatus(string id)
        {
            if (!TransectionTypeExists(id))
            {
                return NotFound();
            }
        
            var transectionType = await GetTransectionTypeById(id);
            transectionType.IsActive = !transectionType.IsActive;
            transectionType.UpdatedBy = GteUserId();
            _context.Update(transectionType);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> SoftDelete(string id)
        {
            if (!TransectionTypeExists(id))
            {
                return NotFound();
            }
        
            var transectionType = await GetTransectionTypeById(id);
            transectionType.IsDeleted = !transectionType.IsDeleted;
            transectionType.UpdatedBy = GteUserId();
            transectionType.DeletedBy = GteUserId();
            transectionType.DeletedAt = DateTime.Now;
            _context.Update(transectionType);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> Restore(string id)
        {
            if (!TransectionTypeExists(id))
            {
                return NotFound();
            }
        
            var transectionType = await GetTransectionTypeById(id);
            transectionType.IsDeleted = !transectionType.IsDeleted;
            transectionType.UpdatedBy = GteUserId();
            _context.Update(transectionType);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        
        private async Task<TransectionType> GetTransectionTypeById(string id)
        {
            var transectionType = await _context.TransectionType.FindAsync(id);
            return transectionType;
        }
    }
}
