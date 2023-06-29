namespace CodeFirstExistingDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatePublishedColumnToCoursesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "DatePublished", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "DatePublished");
        }
    }
}
