namespace CodeFirstExistingDatabase.Migrations
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirstExistingDatabase.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodeFirstExistingDatabase.PlutoContext context)
        {
            context.Authers.AddOrUpdate(a=>a.Name,
                new Authers
                {
                    Name="Authoh 1",
                    Courses=new Collection<Course>()
                    {
                         new Course(){Name="Course for Author 1",Description="Description"}
                    }
                 });        
        
        }
    }
}
