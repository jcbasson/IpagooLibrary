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

                    return command.MapToDto();
                }
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }
        }

        public void AddBooks(List<BookDTO> books)
        {
            try
            {
                using (var trans = _iAdoNetContext.CreateTransaction())
                {
                    try
                    {
                        using (var cmd = _iAdoNetContext.CreateCommand(trans))
                        {
                            foreach(BookDTO book in books)
                            {
                                _iBookRepositoryQuery.AddBook(book, cmd);                              
                            }
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
    }
}
