using Creator.DirectBooking.Api.Service.Utility.Interfaces;
using IpagooLibrary.Models.DTO;
using IpagooLibrary.Repository.Respositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace IpagooLibrary.Service.Services
{
    public class ExpressLibraryService
    {
        protected LibraryBooksRequest GenerateLibraryBookRequest(BookFilter bookFilter)
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

        protected LibraryDTO ProcessExternalLibraryResponse(HttpResponseMessage libraryResponse)
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

        protected LibraryDTO GetBooksFromLibraryExpress(IUrlHttpClient urlHttpClient, IBookRepository bookRepository, BookFilter bookFilter)
        {
            try
            {
                var libraryRequest = GenerateLibraryBookRequest(bookFilter);

                //Go fetch from external service ExpressLibraryApi
                var libraryResponse = urlHttpClient.HttpGet(libraryRequest).Result;

                var libraryDto = ProcessExternalLibraryResponse(libraryResponse);

                if (libraryDto != null && libraryDto.Books != null && libraryDto.Books.Count > 0)
                {
                    try
                    {
                        var newLibraryDto = new LibraryDTO();

                        var ourbooks = bookRepository.AddBooks(libraryDto.Books);

                        newLibraryDto.Books = ourbooks;
                        newLibraryDto.TotalBooks = ourbooks.Count;

                        return newLibraryDto;
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
