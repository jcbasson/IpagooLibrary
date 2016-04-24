using IpagooLibrary.Models.DTO;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpagooLibrary.UI.Hubs
{
    public class LibraryHub : Hub
    {
        //Broacasts to all clients
        public void CreateBookLender(BookLender bookLender)
        {
            if(bookLender != null 
                && !string.IsNullOrWhiteSpace(bookLender.BookISBN) 
                && !string.IsNullOrWhiteSpace(bookLender.FriendName)
                && !string.IsNullOrWhiteSpace(bookLender.BorrowDate))
            {
                Clients.Caller.BookLenderCreateResult("One or more fields were invalid...");
            }
            else
            {
                Clients.All.BookLenderCreateResult("Book was successfully checked out");
            }
        }
    }
}