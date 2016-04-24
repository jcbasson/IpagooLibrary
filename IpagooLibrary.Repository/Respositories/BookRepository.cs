using IpagooLibrary.Models.DTO;
using IpagooLibrary.Repository.Extensions;
using IpagooLibrary.Repository.Respositories.Interfaces;
using System;
using System.Collections.Generic;

namespace IpagooLibrary.Repository.Respositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IBookRepositoryQuery _iBookRepositoryQuery;
        private readonly IAdoNetContext _iAdoNetContext;

        public BookRepository(IBookRepositoryQuery iBookRepositoryQuery, IAdoNetContext iAdoNetContext)
        {
            _iBookRepositoryQuery = iBookRepositoryQuery;
            _iAdoNetContext = iAdoNetContext;
        }

        public LibraryDTO FilterBooks(BookFilter bookFilter)
        {
            try
            {
                using (var cmd = _iAdoNetContext.CreateCommand())
                {
                    var command = _iBookRepositoryQuery.FilterBooks(bookFilter, cmd);

                    if (command == null) return null;

                    return command.MapToLibraryDto();
                }
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }
        }

        public List<BookDTO> AddBooks(List<BookDTO> books)
        {
            try
            {
                var ourBooks = new List<BookDTO>();

                using (var trans = _iAdoNetContext.CreateTransaction())
                {
                    try
                    {
                        using (var cmd = _iAdoNetContext.CreateCommand(trans))
                        {
                            foreach(BookDTO book in books)
                            {
                                var command = _iBookRepositoryQuery.AddBook(book, cmd);

                                var latestBookAdded = command.MapToBookDto();

                                if(latestBookAdded != null)
                                {
                                    ourBooks.Add(latestBookAdded);
                                }
                            }
                            trans.Commit();
                        }
                        return ourBooks;
                    }
                    catch (Exception ex)
                    {
                        //TODO: Log this the exception information along with the method details to the database for Error tracing
                        //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                        trans.Rollback();
                        throw ex;
                    }

                }
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }
        }

        public void CreateBookLender(BookLender bookLender)
        {
            try
            {
                using (var trans = _iAdoNetContext.CreateTransaction())
                {
                    try
                    {
                        using (var cmd = _iAdoNetContext.CreateCommand(trans))
                        {
                            _iBookRepositoryQuery.CreateBookLender(bookLender, cmd);

                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO: Log this the exception information along with the method details to the database for Error tracing
                        //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                throw ex;
            }
        }

        public void ReturnBook(ReturnBook returnBook)
        {
            try
            {
                using (var trans = _iAdoNetContext.CreateTransaction())
                {
                    try
                    {
                        using (var cmd = _iAdoNetContext.CreateCommand(trans))
                        {
                            _iBookRepositoryQuery.ReturnBook(returnBook, cmd);

                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO: Log this the exception information along with the method details to the database for Error tracing
                        //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                throw ex;
            }
        }
    }
}
