using IpagooLibrary.Models.DTO;
using System.Collections.Generic;

namespace IpagooLibrary.Repository.Respositories.Interfaces
{
    public interface IBookRepository 
    {
        LibraryDTO FilterBooks(BookFilter bookFilter);
        List<BookDTO> AddBooks(List<BookDTO> books);
        void CreateBookLender(BookLender bookLender);
        void ReturnBook(ReturnBook returnBook);
    }
}
