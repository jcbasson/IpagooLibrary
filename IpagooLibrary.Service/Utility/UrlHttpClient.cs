using IpagooLibrary.Models.DTO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Creator.DirectBooking.Api.Service.Utility.Interfaces;

namespace Creator.DirectBooking.Api.Service.Utility
{
    public class UrlHttpClient : IUrlHttpClient
    {
        private readonly HttpClient _client;

        public UrlHttpClient()
        {
            _client = new HttpClient();
        }

        public Task<HttpResponseMessage> HttpGet(LibraryBooksRequest libraryBooksRequest)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            libraryBooksRequest.RequestHeaders.ToList().ForEach(requestHeader =>
            {
                _client.DefaultRequestHeaders.Add(requestHeader.Name, requestHeader.Value);
            });
            return _client.GetAsync(libraryBooksRequest.RequestUrl);
        }

        
    }
}