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

namespace PosSustemUIU.Controllers {
    public class CustomerController : BaseCotroller {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public CustomerController (ApplicationDbContext context, IHostingEnvironment environment) {
            _context = context;
            _environment = environment;

        }

        // GET: Customer
        public async Task<IActionResult> Index () {
            return View (await _context.Customers.ToListAsync ());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details (string id) {
            if (id == null) {
                return NotFound ();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync (m => m.Id == id);
            if (customer == null) {
                return NotFound ();
            }

            return View (customer);
        }

        // GET: Customer/Create
        [HttpGet, ActionName ("Create")]
        public async Task<IActionResult> Create () {
            ViewBag.Areas = await _context.Areas.ToListAsync();
            return View ();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,FirstName,LastName,Email,PhoneNumber,OtherContact,Address,Description,Image,IsActive, CustomerAreaId")] Customer customer) {
            if (ModelState.IsValid) {
                //upload image 
                var files = HttpContext.Request.Form.Files;
                if (files != null) {
                    var fileNames = UploadFiles (_environment, files, "customers");
                    if (fileNames.Count > 0) {
                        customer.Image = fileNames[0];
                    }
                }

                //save
                if(customer.CustomerAreaId != null){
                    customer.Area = await _context.Areas.FindAsync(customer.CustomerAreaId);
                }
                customer.CreatedBy = GteUserId ();
                _context.Add (customer);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            ViewBag.Areas = await _context.Areas.ToListAsync();
            return View (customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit (string id) {
            if (id == null) {
                return NotFound ();
            }

            var customer = await _context.Customers.FindAsync (id);
            if (customer == null) {
                return NotFound ();
            }
            ViewBag.Areas = await _context.Areas.ToListAsync();
            return View (customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (string id, [Bind ("Id,FirstName,LastName,Email,PhoneNumber,OtherContact,Address,Description,Image,IsActive, CustomerAreaId")] Customer customer) {
            if (id != customer.Id) {
                return NotFound ();
            }

            if (ModelState.IsValid) {
                try {
                    //upload image 
                    var files = HttpContext.Request.Form.Files;
                    if (files != null) {
                        var fileNames = UploadFiles (_environment, files, "customers");
                        if (fileNames.Count > 0) {
                            customer.Image = fileNames[0];
                        }
                    }

                    //update
                    if (customer.CustomerAreaId != null)
                    {
                        customer.Area = await _context.Areas.FindAsync(customer.CustomerAreaId);
                    }
                    customer.UpdatedBy = GteUserId ();
                    _context.Update (customer);
                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    if (!CustomerExists (customer.Id)) {
                        return NotFound ();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            ViewBag.Areas = await _context.Areas.ToListAsync();
            return View (customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete (string id) {
            if (id == null) {
                return NotFound ();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync (m => m.Id == id);
            if (customer == null) {
                return NotFound ();
            }

            return View (customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (string id) {
            var customer = await _context.Customers.FindAsync (id);
            _context.Customers.Remove (customer);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool CustomerExists (string id) {
            return _context.Customers.Any (e => e.Id == id);
        }

        public override async Task<IActionResult> ChangeActiveStatus (string id) {
            if (!CustomerExists (id)) {
                return NotFound ();
            }

            var customer = await GetCustomerById (id);
            customer.IsActive = !customer.IsActive;
            customer.UpdatedBy = GteUserId ();
            _context.Update (customer);
            await _context.SaveChangesAsync ();

            return RedirectToAction (nameof (Index));
        }

        public override async Task<IActionResult> SoftDelete (string id) {
            if (!CustomerExists (id)) {
                return NotFound ();
            }

            var customer = await GetCustomerById (id);
            customer.IsDeleted = !customer.IsDeleted;
            customer.UpdatedBy = GteUserId ();
            customer.DeletedBy = GteUserId ();
            // customer.DeletedAt = DateTime.Now;
            _context.Update (customer);
            await _context.SaveChangesAsync ();

            return RedirectToAction (nameof (Index));
        }

        public override async Task<IActionResult> Restore (string id) {
            if (!CustomerExists (id)) {
                return NotFound ();
            }

            var customer = await GetCustomerById (id);
            customer.IsDeleted = !customer.IsDeleted;
            customer.UpdatedBy = GteUserId ();
            _context.Update (customer);
            await _context.SaveChangesAsync ();

            return RedirectToAction (nameof (Index));
        }

        private async Task<Customer> GetCustomerById (string id) {
            var customer = await _context.Customers.FindAsync (id);
            return customer;
        }
    }
}