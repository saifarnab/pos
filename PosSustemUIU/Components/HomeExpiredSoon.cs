using Microsoft.AspNetCore.Mvc;
using PosSustemUIU.ComponentsModel;
using PosSustemUIU.Data;
using PosSustemUIU.Models.BLL;

namespace PosSustemUIU.Components
{
    public class HomeExpiredSoon : ViewComponent
    {

        private readonly ApplicationDbContext _context;
        private TransectionManager _manager;

        public HomeExpiredSoon(ApplicationDbContext context)
        {
            _context = context;
            _manager = new TransectionManager(_context);
        }

        public async System.Threading.Tasks.Task<IViewComponentResult> InvokeAsync()
        {
            var stocks = await _manager.GetProductExpiredSoonAsync();
            return View("Default", stocks);
        }
    }
}