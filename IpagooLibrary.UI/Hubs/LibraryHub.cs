using IpagooLibrary.Models.DTO;
using IpagooLibrary.Service.Interfaces;
using IpagooLibrary.UI.Models;
using Microsoft.AspNet.SignalR;
using System;

namespace IpagooLibrary.UI.Hubs
{
    public class LibraryHub : Hub
    {
        private readonly IBookService _iBookService;

        public LibraryHub(IBookService iBookService)
        {
            _iBookService = iBookService;
        }

        //Broacasts to all clients
        public void CheckOutBook(BookLender bookLender)
        {
            try
            {
                BookBorrowResult bookBorrowResult = new BookBorrowResult();

                if (bookLender == null
                    || string.IsNullOrWhiteSpace(bookLender.BookISBN)
                    || string.IsNullOrWhiteSpace(bookLender.FriendName)
                    || string.IsNullOrWhiteSpace(bookLender.BorrowDate))
                {
                    bookBorrowResult.Status = "Validation Error";
                }
                else
                {
                    _iBookService.CreateBookLender(bookLender);
                    bookBorrowResult.Status = "Success";
                    bookBorrowResult.BookISBN = bookLender.BookISBN;
                }

                Clients.All.CheckOutBookResult(bookBorrowResult);
            }
            catch (Exception ex)
            {
                //Log this error ex

                //This is done to allow the exception to flow to client for error notification
                throw new HubException();
            }
        }

        public void CheckInBook(ReturnBook returnBook)
        {
            try
            {        
                _iBookService.ReturnBook(returnBook);

                BookBorrowResult bookBorrowResult = new BookBorrowResult();
                bookBorrowResult.Status = "Success";
                bookBorrowResult.BookISBN = returnBook.ISBN;
                
                Clients.All.CheckInBookResult(bookBorrowResult);
            }
            catch (Exception ex)
            {
                //Log this error ex

                //This is done to allow the exception to flow to client for error notification
                throw new HubException();
            }
        }
    }
}