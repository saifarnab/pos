using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PosSustemUIU.Constants;

namespace PosSustemUIU.ComponentsModel
{
    public class DataTableModel
    {
        public string Url { get; set; }
        public string Heading { get; set; }
        public string ImageDir { get; set; }
        public string Controller { get; set; }
        public bool ShowStatus { get; set; }
        public bool CreateButton { get; set; }
        public List<string> Columns { get; set; }
        public List<TableActions> Actions { get; set; }
        public List<string> Keys { get; set; }
        public IEnumerable<dynamic> TableData { get; set; }
    }
}