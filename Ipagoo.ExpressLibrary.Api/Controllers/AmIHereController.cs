using System.Web.Http;

namespace Ipagoo.ExpressLibrary.Api.Controllers
{
    //Use only for unit testing
    public class AmIHereController : ApiController
    {
        public bool GetIAmHere()
        {
            return true;
        }
    }
}
