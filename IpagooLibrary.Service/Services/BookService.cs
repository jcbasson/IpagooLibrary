using IpagooLibrary.Repository.Respositories.Interfaces;
using IpagooLibrary.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpagooLibrary.Models.DTO;
using Creator.DirectBooking.Api.Service.Utility;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace IpagooLibrary.Service.Services
{
    public class BookService : IBookService
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
                //If they have provide the ISBN number or author name and book title 
                //then we can uniquely identify the book and don't have worry about not returning up to date book lists
                //as in the case of searching on genre and author respectively i.e. there could be new books for that genre and author coming sitting in the external source
                //that we have not loaded into our database yet
                if (bookFilter != null
                    && (!string.IsNullOrWhiteSpace(bookFilter.ISBN)
                    || (!string.IsNullOrWhiteSpace(bookFilter.AuthorName) && !string.IsNullOrWhiteSpace(bookFilter.Title))))
                {
                    libraryDto = _bookRepository.FilterBooks(bookFilter);

                    if (libraryDto == null || libraryDto.Books == null || libraryDto.Books.Count() < 1)
                    {
                        libraryDto = GetBooksFromLibraryExpress(bookFilter);
                    }
                }
                else
                {
                    libraryDto = GetBooksFromLibraryExpress(bookFilter);
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

        private LibraryBooksRequest GenerateLibraryBookRequest(BookFilter bookFilter)
        {
            try
            {
                LibraryBooksRequest libraryBooksRequest = new LibraryBooksRequest();
                libraryBooksRequest.RequestUrl = "http://localhost:2320/Books";

                if (bookFilter != null)
                {
                    List<string> urlParamerters = new List<string>();

                    if (!string.IsNullOrWhiteSpace(bookFilter.ISBN))
                    {
                        urlParamerters.Add(string.Format("ISBN={0}", bookFilter.ISBN));
                    }
                    if (!string.IsNullOrWhiteSpace(bookFilter.Title))
                    {
                        urlParamerters.Add(string.Format("Title={0}", bookFilter.Title));
                    }
                    if (!string.IsNullOrWhiteSpace(bookFilter.AuthorName))
                    {
                        urlParamerters.Add(string.Format("AuthorName={0}", bookFilter.AuthorName));
                    }
                    if (!string.IsNullOrWhiteSpace(bookFilter.Genre))
                    {
                        urlParamerters.Add(string.Format("Genre={0}", bookFilter.Genre));
                    }

                    libraryBooksRequest.RequestUrl = urlParamerters.Count() > 0 ?
                        string.Format("{0}?{1}", libraryBooksRequest.RequestUrl, string.Join("&", urlParamerters))
                        : libraryBooksRequest.RequestUrl;
                }

                libraryBooksRequest.RequestHeaders = new List<RequestHeader>();

                return libraryBooksRequest;
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }
        }

        private LibraryDTO ProcessExternalLibraryResponse(HttpResponseMessage libraryResponse)
        {
            try
            {
                if (libraryResponse == null || !libraryResponse.StatusCode.Equals(HttpStatusCode.OK)) throw new HttpRequestException("This external api is rubbish and I need to log more details:)");

                var responseContent = libraryResponse.Content.ReadAsStringAsync().Result;
                var libraryDto = JsonConvert.DeserializeObject<LibraryDTO>(responseContent);

                return libraryDto;
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }
        }

        private LibraryDTO GetBooksFromLibraryExpress(BookFilter bookFilter)
        {
            try
            {
                var libraryRequest = GenerateLibraryBookRequest(bookFilter);

                //Go fetch from external service ExpressLibraryApi
                var libraryResponse = _urlHttpClient.HttpGet(libraryRequest).Result;

                var libraryDto = ProcessExternalLibraryResponse(libraryResponse);

                if (libraryDto != null && libraryDto.Books != null && libraryDto.Books.Count > 0)
                {
                    try
                    {
                        _bookRepository.AddBooks(libraryDto.Books);
                    }
                    catch (Exception ex)
                    {
                        //TODO: Log this the exception information along with the method details to the database for Error tracing
                        //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                        return null;
                    }
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
    }
}
