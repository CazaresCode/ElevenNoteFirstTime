namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CateogryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CateogryId);
            
            AddColumn("dbo.Note", "CateogryId", c => c.Int());
            AlterColumn("dbo.Note", "Title", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Note", "CateogryId");
            AddForeignKey("dbo.Note", "CateogryId", "dbo.Category", "CateogryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "CateogryId", "dbo.Category");
            DropIndex("dbo.Note", new[] { "CateogryId" });
            AlterColumn("dbo.Note", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Note", "CateogryId");
            DropTable("dbo.Category");
        }
    }
}
