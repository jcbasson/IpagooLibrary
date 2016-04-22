using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpagooLibrary.UI.Models
{
    public class BookViewModel
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
        public int LenderID { get; set; }
        public bool IsOut {
            get{
                return LenderID > 0; 
            }
        }
    }
}