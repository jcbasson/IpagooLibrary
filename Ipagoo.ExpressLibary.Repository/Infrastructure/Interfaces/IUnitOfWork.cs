using System.Threading.Tasks;

namespace Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}