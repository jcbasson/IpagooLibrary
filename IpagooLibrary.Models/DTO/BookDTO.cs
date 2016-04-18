using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpagooLibrary.Models.DTO
{
    public class BookDTO
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
    }
}
