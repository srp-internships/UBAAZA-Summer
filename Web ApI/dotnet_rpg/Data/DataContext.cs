
using System.Data.Common;

namespace dotnet_rpg.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
                
        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();

    }
}
