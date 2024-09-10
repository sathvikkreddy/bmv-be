using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class BmvContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"server=.\sqlexpress;initial catalog=bmv;user id=sa;password=Pass@123;trustservercertificate=true");
        }
    }
}
