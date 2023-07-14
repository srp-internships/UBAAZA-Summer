
using System.Data.Common;

namespace dotnet_rpg.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData
                (
                new Skill { Id=1,Name="Fireball" , Demage=30},
                new Skill { Id=2,Name="Frenzy" , Demage=20},
                new Skill { Id=3,Name="Blizzard" , Demage=50}
                );
        }
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();

    }
}
