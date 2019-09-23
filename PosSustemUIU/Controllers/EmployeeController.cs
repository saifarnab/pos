using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSustemUIU.Data;
using PosSustemUIU.Models;
using PosSustemUIU.ViewModels;

namespace PosSustemUIU.Controllers
{
    [Authorize]
    public class EmployeeController : BaseCotroller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment _environment;
        private readonly string _roleName = "Employee";
         
        
        public EmployeeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHostingEnvironment environment):base()
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._environment = environment;
        }
        
        // GET: {Controller}
        public async Task<IActionResult> Index()
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View( _userManager.Users);
        }
        
        // GET: {Controller}/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
        
            return View(user);
        }
        
        // GET: {Controller}/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: {Controller}/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeVM employee)
        {
            if (!ModelState.IsValid)  return View(employee);

            //upload image is exist
            var files = HttpContext.Request.Form.Files;
            string fileName = null;
            if (files != null)
            {
                var fileNames = UploadFiles(_environment, files, "users");
                if(fileNames.Count > 0){
                    fileName = fileNames[0];
                }
            }
            //save employee
            var user = new ApplicationUser
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                UserName = employee.Email,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DateOfBirth = employee.DateOfBirth,
                Image = fileName,
                Address = employee.Address,
                Description = employee.Description,
                IsActive = employee.IsActive
            };
            var res = await _userManager.CreateAsync(user, employee.Password);
            var roleRes = await _userManager.AddToRoleAsync(user, _roleName);
            return RedirectToAction(nameof(Index));
        }
        
        // GET: {Controller}/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            // var employee = new EmployeeVM{
            //     FirstName = user.FirstName,
            //     LastName = user.LastName,
            //     Email = user.Email,
            //     PhoneNumber = user.PhoneNumber,
            //     Address = user.Address,
            //     Description = user.Description,
            //     IsActive = user.IsActive,
            //     DateOfBirth = user.DateOfBirth,

            // };
            return View(user);
        }
        
        // POST: {Controller}/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(user.Id))
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
            return View(user);
        }
        
        
        // POST: {Controller}/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
        
        private async Task<bool> UserExists(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user == null ? false : true;
        }

        [HttpGet]
        public override async Task<IActionResult> ChangeActiveStatus(string id)
        {
            // id = id as string;
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> SoftDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsDeleted = !user.IsDeleted;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Restore(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsDeleted = !user.IsDeleted;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}