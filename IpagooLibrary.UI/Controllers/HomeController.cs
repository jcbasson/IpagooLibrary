using IpagooLibrary.Models.DTO;
using IpagooLibrary.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IpagooLibrary.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _iBookService;

        public HomeController(IBookService iBookService)
        {
            _iBookService = iBookService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBooks(BookFilter bookFilter)
        {
            var books = _iBookService.FilterBooks(bookFilter);

            return new JsonResult() { Data = books, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult CreateBookLender(BookLender bookLender)
        {
            _iBookService.CreateBookLender(bookLender);

            return new JsonResult() { Data = string.Format("{0} was successfully saved.", bookLender.FriendName) };
        }
    }
}