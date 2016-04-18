using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpagooLibrary.Models.DTO
{
    public class LibraryDTO
    {
        public List<BookDTO> Books { get; set; }
        public int TotalBooks { get; set; }
    }
}
