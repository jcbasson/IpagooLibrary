using Ipagoo.ExpressLibrary.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipagoo.ExpressLibrary.Models.DTO
{
    public class ExpressLibraryResponse
    {
        public IEnumerable<Book> Books { get; set; }
        public int BooksSearchIndex { get; set; }
        public int TotalRequestedBooks { get; set; }
        public int TotalExistingBooks
        {
            get
            {
                return Books.Count();
            }
        }
    }
}
