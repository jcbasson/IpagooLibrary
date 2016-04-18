using IpagooLibrary.Repository.Infrastructure.Interfaces;
using System.Data;

namespace IpagooLibrary.Repository
{
    public interface IAdoNetContext
    {
        IDbCommand CreateCommand();
        IDbCommand CreateCommand(IDbTransaction iDbTransaction);
        IDbTransaction CreateTransaction();
    }
}
