using System.Data.Entity.ModelConfiguration;

namespace Ipagoo.ExpressLibrary.Models.DB.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ISBN)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AuthorName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Genre)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Book");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ISBN).HasColumnName("ISBN");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.AuthorName).HasColumnName("AuthorName");
            this.Property(t => t.Genre).HasColumnName("Genre");
        }
    }
}
