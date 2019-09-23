using Microsoft.AspNetCore.Mvc;
using PosSustemUIU.ComponentsModel;

namespace PosSustemUIU.Components
{
    public class DataTable : ViewComponent
    {
        // private readonly dynamic _list;

        // public DataTable(dynamic list)
        // {
        //     this._list = list;
        // }

        public IViewComponentResult Invoke(DataTableModel model)
        {
            return View("Default",model);
        }
    }
}