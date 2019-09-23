using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PosSustemUIU.Data;

namespace PosSustemUIU.Models.BLL
{
    public class TransectionManager
    {
        private readonly ApplicationDbContext _context;

        public TransectionManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<List<Transection>> GetStockReportsAsync(){
            var transections = _context.Transections.Where(t => t.TransectionTypeId == "bda54eb3-c4ea-4a52-a488-9fdaf2bb6e8d" && t.RemainingQuantity > 0).Include(t => t.Product).Include(t => t.TransectionType);
            
            return await transections.ToListAsync();
        }

        public async System.Threading.Tasks.Task<List<Transection>> GetLowInventoryAsync(){
            var transections = _context.Transections.Where(t => t.TransectionTypeId == "bda54eb3-c4ea-4a52-a488-9fdaf2bb6e8d" && t.RemainingQuantity < 10).Include(t => t.Product).Include(t => t.TransectionType);
            
            return await transections.ToListAsync();
        }

        public async System.Threading.Tasks.Task<List<Transection>> GetProductExpiredSoonAsync(){
            var date = DateTime.Now.AddDays(7);
            var transections = _context.Transections.Where(t => t.TransectionTypeId == "bda54eb3-c4ea-4a52-a488-9fdaf2bb6e8d" && t.ExpireDate < date).Include(t => t.Product).Include(t => t.TransectionType);
            
            return await transections.ToListAsync();
        }

        public async System.Threading.Tasks.Task<List<Transection>> GetTodaysTransectionAsync(){
            var date = DateTime.Today;
            var transections = _context.Transections.Where(t => t.ExpireDate == date).Include(t => t.Product).Include(t => t.TransectionType);
            
            return await transections.ToListAsync();
        }
    }
}