namespace Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces
{
    public interface IDatabaseFactory
    {
        DataContext Get();
    }
}