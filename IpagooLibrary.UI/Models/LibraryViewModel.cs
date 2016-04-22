using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpagooLibrary.UI.Models
{
    public class LibraryViewModel
    {
        public List<BookViewModel> Books { get; set; }
        public int TotalBooks { get; set; }
    }
}