using AutoMapper;
using IpagooLibrary.Models.DTO;
using IpagooLibrary.Service.Interfaces;
using IpagooLibrary.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace IpagooLibrary.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _iBookService;

        private readonly IMapper _mapper;

        public HomeController(IBookService iBookService, IMapper mapper)
        {
            _iBookService = iBookService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBooks(BookSearch bookSearch)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors)
                }, JsonRequestBehavior.AllowGet);
            }
            var bookFilter = _mapper.Map<BookSearch, BookFilter>(bookSearch);
            if (bookFilter == null) return new JsonResult() { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var libaryDto = _iBookService.FilterBooks(bookFilter);
            if (libaryDto == null) return new JsonResult() { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var libaryViewModel = _mapper.Map<LibraryDTO, LibraryViewModel>(libaryDto);

            return new JsonResult() { Data = libaryViewModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult CreateBookLender(BookLender bookLender)
        {
            _iBookService.CreateBookLender(bookLender);

            return new JsonResult() { Data = string.Format("{0} was successfully saved.", bookLender.FriendName) };
        }
    }
}