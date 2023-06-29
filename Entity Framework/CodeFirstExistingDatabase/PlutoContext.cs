using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CodeFirstExistingDatabase
{
    public partial class PlutoContext : DbContext
    {
        public PlutoContext()
            : base("name=PlutoContext")
        {
        }

        public virtual DbSet<Authers> Authers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(t => t.Description)
                .IsRequired();

            modelBuilder.Entity<Authers>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.Authers)
                .HasForeignKey(e => e.Auther_Id);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Courses)
                .Map(m => m.ToTable("TagCourses").MapLeftKey("Course_Id").MapRightKey("Tag_Id"));
        }
    }
}
