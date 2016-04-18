using Ipagoo.ExpressLibrary.Models.DB;
using Ipagoo.ExpressLibrary.Models.DB.Mapping;
using System.Data.Entity;

namespace Ipagoo.ExpressLibary.Repository
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer<DataContext>(null);
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual void Commit()
        {
            SaveChanges();
        }


        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
        }

        

       

    }
}
