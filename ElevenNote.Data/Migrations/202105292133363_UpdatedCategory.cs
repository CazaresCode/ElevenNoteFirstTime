namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Note", "CateogryId", "dbo.Category");
            DropIndex("dbo.Note", new[] { "CateogryId" });
            AddColumn("dbo.Category", "CategoryName", c => c.String(nullable: false));
            AlterColumn("dbo.Note", "CateogryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Note", "CateogryId");
            AddForeignKey("dbo.Note", "CateogryId", "dbo.Category", "CateogryId", cascadeDelete: true);
            DropColumn("dbo.Category", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Category", "Name", c => c.String(nullable: false));
            DropForeignKey("dbo.Note", "CateogryId", "dbo.Category");
            DropIndex("dbo.Note", new[] { "CateogryId" });
            AlterColumn("dbo.Note", "CateogryId", c => c.Int());
            DropColumn("dbo.Category", "CategoryName");
            CreateIndex("dbo.Note", "CateogryId");
            AddForeignKey("dbo.Note", "CateogryId", "dbo.Category", "CateogryId");
        }
    }
}
