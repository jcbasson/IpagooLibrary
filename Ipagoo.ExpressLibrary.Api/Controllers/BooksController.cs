using Ipagoo.ExpressLibrary.Models.DTO;
using Ipagoo.ExpressLibrary.Service.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;

namespace Ipagoo.ExpressLibrary.Api.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Successful : 200 OK + ExpressLibraryResponse containting list of books and paging information
        /// No Content : 204 + Nothing
        /// All other errors : 500 + Short Description
        /// </summary>
        [HttpGet]
        [EnableCors(origins: "http://localhost:4561/", headers: "*", methods: "GET")]
        public HttpResponseMessage Get([FromUri] BookFilter bookFilter)
        {
            try
            {
                var expressLibraryResponse = _bookService.FindBooks(bookFilter);
                
                return expressLibraryResponse != null ? Request.CreateResponse(HttpStatusCode.OK, expressLibraryResponse) : Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                //LOG this error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Service is currently unavailable");
            }          
        }

        //public class CustomAttribute : ActionFilterAttribute
        //{
            
        //    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        //    {
        //        base.OnActionExecuted(actionExecutedContext);
        //    }
        //}

    }
}
