using API_eCommerceProject.Model.Category;
using Microsoft.EntityFrameworkCore;

namespace API_eCommerceProject.Data
{
    public class ApplicationDBContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=RSHOP;" +
                    "Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}
