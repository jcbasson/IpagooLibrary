using IpagooLibrary.Models.DTO;

namespace IpagooLibrary.Service.Interfaces
{
    public interface IBookService
    {
        LibraryDTO FilterBooks(BookFilter bookFilter);
        void CreateBookLender(BookLender bookLender);
        void ReturnBook(ReturnBook returnBook);
    }
}
