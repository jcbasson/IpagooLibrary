using Creator.DirectBooking.Api.Repository.Respositories.Interfaces;
using Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces;
using Ipagoo.ExpressLibrary.Models.DB;
using Ipagoo.ExpressLibrary.Models.DTO;
using Ipagoo.ExpressLibrary.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Ipagoo.ExpressLibrary.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public ExpressLibraryResponse FindBooks(BookFilter bookFilter)
        {
            try
            {
                IList<Book> books = new List<Book>();
                ExpressLibraryResponse expressLibraryResponse = new ExpressLibraryResponse();
                
                if(bookFilter == null || 
                    (string.IsNullOrEmpty(bookFilter.ISBN) && string.IsNullOrEmpty(bookFilter.Title) 
                    && string.IsNullOrEmpty(bookFilter.AuthorName) && string.IsNullOrEmpty(bookFilter.Genre)))
                {
                    books = _bookRepository.GetAll();
                    if (books == null) return null;
                }
                else
                {
                    var lambdaQuery = GenerateWhereClause(bookFilter);
                    if (lambdaQuery == null) return null;

                    books = _bookRepository.GetMany(lambdaQuery);
                    if (books == null) return null;
                }
  
                expressLibraryResponse.Books = books;

                return expressLibraryResponse;
            }
            catch (Exception ex)
            {
                //LOG ERROR OCCURRED ON SERVICE LAYER 
                return null;
            }
        }

        private Expression<Func<Book, bool>> GenerateWhereClause(BookFilter bookFilter)
        {
            try
            {
                ParameterExpression argumentParam = Expression.Parameter(typeof(Book), "books");

                Expression propertyISBN = Expression.Property(argumentParam, "ISBN");
                Expression propertyTitle = Expression.Property(argumentParam, "Title");
                Expression propertyAuthorName = Expression.Property(argumentParam, "AuthorName");
                Expression propertyGenre = Expression.Property(argumentParam, "Genre");

                MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });

                var valISBN = Expression.Constant(bookFilter.ISBN, typeof(string));
                var valAuthorName = Expression.Constant(bookFilter.AuthorName, typeof(string));
                var valTitle = Expression.Constant(bookFilter.Title, typeof(string));
                var valGenre = Expression.Constant(bookFilter.Genre, typeof(string));

                var startsWithExpISBN = Expression.Call(propertyISBN, startsWithMethod, valISBN);
                var startsWithExpTitle = Expression.Call(propertyISBN, startsWithMethod, valAuthorName);
                var startsWithExpAuthorName = Expression.Call(propertyISBN, startsWithMethod, valTitle);
                var startsWithExpGenre = Expression.Call(propertyISBN, startsWithMethod, valGenre);

                Expression firsOrExp = Expression.OrElse(startsWithExpISBN, startsWithExpTitle);
                Expression secondOrExp = Expression.OrElse(firsOrExp, startsWithExpAuthorName);
                Expression finalOrExp = Expression.OrElse(secondOrExp, startsWithExpGenre);


                var lambdaQuery = Expression.Lambda<Func<Book, bool>>(argumentParam, argumentParam);

                return lambdaQuery;
            }
            catch (Exception ex)
            {
                //LOG ERROR OCCURRED ON SERVICE LAYER 
                return null;
            }
        }


    }
}
