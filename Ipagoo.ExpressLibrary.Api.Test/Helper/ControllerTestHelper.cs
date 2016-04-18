using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using Autofac;

namespace Ipagoo.ExpressLibrary.Api.Test.Helper
{
    public static class APIControllerTestHelper
    {
        public static TController ResolveController<TController>(IComponentContext lifetime) where TController : ApiController
        {
            var controller = lifetime.Resolve<TController>();
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return controller;
        }

        public static async Task<T> GetContent<T>(HttpResponseMessage message)
        {
            return await message.Content.ReadAsAsync<T>();
        }

        public static async Task<List<T>> GetContentList<T>(HttpResponseMessage message)
        {
            return await message.Content.ReadAsAsync<List<T>>();
        }
    }
}