using System.Data;

namespace IpagooLibrary.Repository.Infrastructure.Interfaces
{
    public interface IDatabaseFactory
    {
        IDbConnection Create();
    }
}