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
    public class StoreConfigurationController : BaseCotroller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public StoreConfigurationController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: StoreConfiguration
        public async Task<IActionResult> Index()
        {
            return View(await _context.StoreConfigurations.ToListAsync());
        }

        // GET: StoreConfiguration/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeConfiguration = await _context.StoreConfigurations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeConfiguration == null)
            {
                return NotFound();
            }

            return View(storeConfiguration);
        }

        // GET: StoreConfiguration/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreConfiguration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Sologan,Address,Logo,MainContact,OtherContact,Email,Website,IsActive,Meta")] StoreConfiguration storeConfiguration)
        {
            if (ModelState.IsValid)
            {
                //upload image 
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    var fileNames = UploadFiles(_environment, files, "store-logos");
                    if (fileNames.Count > 0)
                    {
                        storeConfiguration.Logo = fileNames[0];
                    }
                }
                storeConfiguration.CreatedBy = GteUserId();
                _context.Add(storeConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeConfiguration);
        }

        // GET: StoreConfiguration/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeConfiguration = await _context.StoreConfigurations.FindAsync(id);
            if (storeConfiguration == null)
            {
                return NotFound();
            }
            return View(storeConfiguration);
        }

        // POST: StoreConfiguration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Sologan,Address,Logo,MainContact,OtherContact,Email,Website,IsActive,Meta")] StoreConfiguration storeConfiguration)
        {
            if (id != storeConfiguration.Id)
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
                        var fileNames = UploadFiles(_environment, files, "store-logos");
                        if (fileNames.Count > 0)
                        {
                            storeConfiguration.Logo = fileNames[0];
                        }
                    }
                    storeConfiguration.UpdatedBy = GteUserId();
                    _context.Update(storeConfiguration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreConfigurationExists(storeConfiguration.Id))
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
            return View(storeConfiguration);
        }

        // GET: StoreConfiguration/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeConfiguration = await _context.StoreConfigurations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeConfiguration == null)
            {
                return NotFound();
            }

            return View(storeConfiguration);
        }

        // POST: StoreConfiguration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var storeConfiguration = await _context.StoreConfigurations.FindAsync(id);
            _context.StoreConfigurations.Remove(storeConfiguration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreConfigurationExists(string id)
        {
            return _context.StoreConfigurations.Any(e => e.Id == id);
        }

        public override async Task<IActionResult> ChangeActiveStatus(string id)
        {
            if (!StoreConfigurationExists(id))
            {
                return NotFound();
            }
        
            var storeConfiguration = await GetConfigBuId(id);
            storeConfiguration.IsActive = !storeConfiguration.IsActive;
            storeConfiguration.UpdatedBy = GteUserId();
            _context.Update(storeConfiguration);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> SoftDelete(string id)
        {
            if (!StoreConfigurationExists(id))
            {
                return NotFound();
            }
        
            var storeConfiguration = await GetConfigBuId(id);
            storeConfiguration.IsDeleted = !storeConfiguration.IsDeleted;
            storeConfiguration.UpdatedBy = GteUserId();
            storeConfiguration.DeletedBy = GteUserId();
            storeConfiguration.DeletedAt = DateTime.Now;
            _context.Update(storeConfiguration);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        public override async Task<IActionResult> Restore(string id)
        {
            if (!StoreConfigurationExists(id))
            {
                return NotFound();
            }
        
            var storeConfiguration = await GetConfigBuId(id);
            storeConfiguration.IsDeleted = !storeConfiguration.IsDeleted;
            storeConfiguration.UpdatedBy = GteUserId();
            _context.Update(storeConfiguration);
            await _context.SaveChangesAsync();
        
            return RedirectToAction(nameof(Index));
        }
        
        
        private async Task<StoreConfiguration> GetConfigBuId(string id)
        {
            var storeConfiguration = await _context.StoreConfigurations.FindAsync(id);
            return storeConfiguration;
        }
    }
}
