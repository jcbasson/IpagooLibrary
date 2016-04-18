namespace Ipagoo.ExpressLibrary.Models.DTO
{
    public class BookFilter
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
    }
}
