using IpagooLibrary.Models.DTO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Creator.DirectBooking.Api.Service.Utility.Interfaces
{
    public interface IUrlHttpClient
    {
        Task<HttpResponseMessage> HttpGet(LibraryBooksRequest libraryBooksRequest);
    }
}