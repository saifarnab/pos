using Microsoft.AspNetCore.Mvc;
using PosSustemUIU.ComponentsModel;
using PosSustemUIU.Data;
using PosSustemUIU.Models.BLL;

namespace PosSustemUIU.Components
{
    public class HomeLowInventory : ViewComponent
    {

        private readonly ApplicationDbContext _context;
        private TransectionManager _manager;

        public HomeLowInventory(ApplicationDbContext context)
        {
            _context = context;
            _manager = new TransectionManager(_context);
        }

        public async System.Threading.Tasks.Task<IViewComponentResult> InvokeAsync()
        {
            var stocks = await _manager.GetLowInventoryAsync();
            return View("Default", stocks);
        }
    }
}