using NUnit.Framework;
using Autofac;
using Ipagoo.ExpressLibrary.Api.Test.Config;
using Ipagoo.ExpressLibrary.Api.Test.Helper;
using System.Net.Http;
using System.Net;
using Ipagoo.ExpressLibrary.Api.Controllers;
using Ipagoo.ExpressLibrary.Models.DTO;
using System.Threading.Tasks;

namespace Ipagoo.ExpressLibrary.Api.Test
{
    //Just note the tests have been simplified and are merely here for a proof of concept, thus are no where near comprehensive enough
    [TestFixture]
    public class BooksTest
    {
        private IContainer _container;
        [SetUp]
        public void Setup()
        {
            _container = AutoFacInitialiser.AutoFacSetup();
        }

        //Given a BookController Get End Point and there is a filter object
        //When a GET request 
        //Then an object with a list of people and a pager object should be returned with a 200 Status Code Response
        [TestCase()]
        public async Task GetBookByISBNTest()
        {
            using (var lifetime = _container.BeginLifetimeScope())
            {
                //given
                var controller = APIControllerTestHelper.ResolveController<BooksController>(lifetime);
                var bookFilter = new BookFilter
                {
                    ISBN = "8-888888-8-8"
                };

                //when
                HttpResponseMessage response = controller.Get(bookFilter);

                //then
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
                var returnedObject = await APIControllerTestHelper.GetContent<ExpressLibraryResponse>(response);
                Assert.IsTrue(returnedObject != null, "Could not find any result");
            }
        }

    }
}
