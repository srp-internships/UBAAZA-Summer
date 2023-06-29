namespace CodeFirstExistingDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCategoriesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
              "dbo._Categories",
              c => new
              {
                  Id = c.Int(nullable: false, identity: true),
                  Name = c.String(),
              })
              .PrimaryKey(t => t.Id);
            Sql("INSERT INTO _Categories(Name) SELECT Name From Categories");

            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO Categories(Name) SELECT Name From Categories");
            DropTable("dbo._Categories");
        }
    }
}
