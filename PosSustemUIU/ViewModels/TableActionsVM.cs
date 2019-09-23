using System.Collections.Generic;
using PosSustemUIU.Constants;

namespace PosSustemUIU.ViewModels
{
    public class TableActionsVM
    {
        public List<TableActions> Actions { get; set; }
        public dynamic TableData { get; set; }
        public string Controller { get; set; }



    }
}