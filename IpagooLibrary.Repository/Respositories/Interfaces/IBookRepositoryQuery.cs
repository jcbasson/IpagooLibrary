using IpagooLibrary.Models.DTO;
using System.Data;

namespace IpagooLibrary.Repository.Respositories.Interfaces
{
    public interface IBookRepositoryQuery
    {
        IDbCommand FilterBooks(BookFilter bookFilter, IDbCommand command);
        IDbCommand AddBook(BookDTO book, IDbCommand command);
        void CreateBookLender(BookLender bookLender, IDbCommand command);
        void ReturnBook(ReturnBook returnBook, IDbCommand command);
    }
}
