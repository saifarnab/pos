using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PosSustemUIU.Models;

namespace PosSustemUIU.Controllers
{
    public abstract class BaseCotroller : Controller
    {
        [HttpGet]
        public abstract Task<IActionResult> ChangeActiveStatus(string id);
        public abstract Task<IActionResult> SoftDelete(string id);
        public abstract Task<IActionResult> Restore(string id);

        public List<string> UploadFiles(IHostingEnvironment environment, dynamic files, string folder){

            var fileNames = new List<string>();
            // files = files as HttpContext.Request.Form.Files;

            foreach (var file in files)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;
                var newFileName = string.Empty;

                if (file.Length > 0)
                {
                    var uploads = Path.Combine(environment.WebRootPath, "uploads/"+folder);
                    var newName = GetUniqueFileName(file.FileName);
                    var fullPath = Path.Combine(uploads, newName);
                    file.CopyTo(new FileStream(fullPath, FileMode.Create));

                    fileNames.Add(newName);
                }
            }
            
            return fileNames;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        protected string GteUserId(){
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }


    }
}