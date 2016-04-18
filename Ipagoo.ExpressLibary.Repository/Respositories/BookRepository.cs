using Creator.DirectBooking.Api.Repository.Respositories.Interfaces;
using Ipagoo.ExpressLibary.Repository.Infrastructure;
using Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces;
using Ipagoo.ExpressLibrary.Models.DB;

namespace Creator.DirectBooking.Api.Repository.Respositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
