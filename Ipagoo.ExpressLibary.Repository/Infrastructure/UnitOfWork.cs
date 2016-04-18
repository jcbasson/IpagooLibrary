using System.Data.Entity;
using System.Threading.Tasks;
using Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces;

namespace Ipagoo.ExpressLibary.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dataContext;
        private readonly IDatabaseFactory _databaseFactory;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        protected DbContext DataContext
        {
            get { return _dataContext?? (_dataContext = _databaseFactory.Get()); }
        }

        public Task Commit()
        {
            return DataContext.SaveChangesAsync();
        }
    }
}