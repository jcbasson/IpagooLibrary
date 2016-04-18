using System.Web;
using System.Web.Mvc;

namespace Ipagoo.ExpressLibrary.DataPoint
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
