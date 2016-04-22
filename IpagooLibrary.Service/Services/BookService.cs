using IpagooLibrary.Repository.Respositories.Interfaces;
using IpagooLibrary.Service.Interfaces;
using System;
using System.Linq;
using IpagooLibrary.Models.DTO;
using Creator.DirectBooking.Api.Service.Utility.Interfaces;

namespace IpagooLibrary.Service.Services
{
    public class BookService : ExpressLibraryService, IBookService 
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUrlHttpClient _urlHttpClient;

        public BookService(IBookRepository peopleRepository, IUrlHttpClient iUrlHttpClient)
        {
            _bookRepository = peopleRepository;
            _urlHttpClient = iUrlHttpClient;
        }

        public LibraryDTO FilterBooks(BookFilter bookFilter)
        {
            try
            {
                LibraryDTO libraryDto = null;
                
                if (bookFilter != null
                    && (!string.IsNullOrWhiteSpace(bookFilter.ISBN)
                    || (!string.IsNullOrWhiteSpace(bookFilter.AuthorName) && !string.IsNullOrWhiteSpace(bookFilter.Title))))
                {
                    libraryDto = _bookRepository.FilterBooks(bookFilter);

                    if (libraryDto == null || libraryDto.Books == null || libraryDto.Books.Count() < 1)
                    {
                        libraryDto = GetBooksFromLibraryExpress(_urlHttpClient, _bookRepository, bookFilter);
                    }
                }
                else
                {
                    libraryDto = GetBooksFromLibraryExpress(_urlHttpClient, _bookRepository, bookFilter);
                }
                return libraryDto;
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
                _bookRepository.CreateBookLender(bookLender);
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
