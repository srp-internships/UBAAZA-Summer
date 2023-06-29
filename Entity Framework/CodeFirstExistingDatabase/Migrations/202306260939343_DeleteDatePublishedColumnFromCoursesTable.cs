namespace CodeFirstExistingDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDatePublishedColumnFromCoursesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "DatePublished");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "DatePublished", c => c.DateTime());
        }
    }
}
