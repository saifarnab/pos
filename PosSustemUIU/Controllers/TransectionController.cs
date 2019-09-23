using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PosSustemUIU.Data;
using PosSustemUIU.Models;
using PosSustemUIU.Models.BLL;

namespace PosSustemUIU.Controllers
{
    public class TransectionController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;
        private  TransectionManager _manager;

        public TransectionController(ApplicationDbContext context)
        {
            _context = context;
            _manager = new TransectionManager(_context);
        }

        // GET: Transection
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transections.Include(t => t.Product).Include(t => t.TransectionType);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet("stocks")]
        public async Task<IActionResult> Stocks()
        {
            var stocks = await _manager.GetStockReportsAsync();
            return View(stocks);
        }

        [HttpGet("low-inventory")]
        public async Task<IActionResult> LowInventory()
        {
            var stocks = await _manager.GetLowInventoryAsync();
            return View(stocks);
        }

        // GET: Transection/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transection = await _context.Transections
                .Include(t => t.Product)
                .Include(t => t.TransectionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transection == null)
            {
                return NotFound();
            }

            return View(transection);
        }

        // GET: Transection/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["TransectionTypeId"] = new SelectList(_context.TransectionType, "Id", "Id");
            return View();
        }

        // POST: Transection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,TransectionTypeId,ProductId,Price,Vat,Quantity,RemainingQuantity,ExpireDate,CreatedBy,UpdatedBy,DeletedBy,CreatedAt,DeletedAt")] Transection transection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", transection.ProductId);
            ViewData["TransectionTypeId"] = new SelectList(_context.TransectionType, "Id", "Id", transection.TransectionTypeId);
            return View(transection);
        }

        // GET: Transection/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transection = await _context.Transections.FindAsync(id);
            if (transection == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", transection.ProductId);
            ViewData["TransectionTypeId"] = new SelectList(_context.TransectionType, "Id", "Id", transection.TransectionTypeId);
            return View(transection);
        }

        // POST: Transection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ParentId,TransectionTypeId,ProductId,Price,Vat,Quantity,RemainingQuantity,ExpireDate,CreatedBy,UpdatedBy,DeletedBy,CreatedAt,DeletedAt")] Transection transection)
        {
            if (id != transection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransectionExists(transection.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", transection.ProductId);
            ViewData["TransectionTypeId"] = new SelectList(_context.TransectionType, "Id", "Id", transection.TransectionTypeId);
            return View(transection);
        }

        // GET: Transection/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transection = await _context.Transections
                .Include(t => t.Product)
                .Include(t => t.TransectionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transection == null)
            {
                return NotFound();
            }

            return View(transection);
        }

        // POST: Transection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transection = await _context.Transections.FindAsync(id);
            _context.Transections.Remove(transection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransectionExists(string id)
        {
            return _context.Transections.Any(e => e.Id == id);
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
