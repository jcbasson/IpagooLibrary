using System;
using System.Collections.Generic;
using System.Globalization;
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
        public DateTime BorrowedDateTime
        {
            get
            {
                DateTime date = new DateTime();
                if (!string.IsNullOrWhiteSpace(BorrowDate))
                {
                     date = DateTime.ParseExact(BorrowDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                return date;
            }
        }
        public string Comments { get; set; }
    }
}
