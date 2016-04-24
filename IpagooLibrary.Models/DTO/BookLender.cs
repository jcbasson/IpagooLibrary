using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpagooLibrary.Models.DTO
{
    public class BookLender
    {
        public string FriendName { get; set; }
        public string BookISBN { get; set; }
        public string BorrowDate { get; set; }
        public string Comments { get; set; }
    }
}
