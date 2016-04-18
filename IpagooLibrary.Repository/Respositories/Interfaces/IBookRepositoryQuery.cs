using IpagooLibrary.Models.DTO;
using System.Data;

namespace IpagooLibrary.Repository.Respositories.Interfaces
{
    public interface IBookRepositoryQuery
    {
        IDbCommand FilterBooks(BookFilter bookFilter, IDbCommand command);
        void AddBook(BookDTO book, IDbCommand command);
        void CreateBookLender(BookLender bookLender, IDbCommand command);
    }
}
