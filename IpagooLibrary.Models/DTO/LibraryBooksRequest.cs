using System.Collections.Generic;

namespace IpagooLibrary.Models.DTO
{
    public class LibraryBooksRequest
    {
        public string RequestUrl { get; set; }
        public IList<RequestHeader> RequestHeaders { get; set; }
    }
}
