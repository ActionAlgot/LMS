namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SharedFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        UploaderID = c.String(maxLength: 128),
                        KlassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasses", t => t.KlassID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UploaderID)
                .Index(t => t.UploaderID)
                .Index(t => t.KlassID);
            
            CreateTable(
                "dbo.SubmissionFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        UploaderID = c.String(maxLength: 128),
                        KlassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasses", t => t.KlassID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UploaderID)
                .Index(t => t.UploaderID)
                .Index(t => t.KlassID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmissionFiles", "UploaderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubmissionFiles", "KlassID", "dbo.Klasses");
            DropForeignKey("dbo.SharedFiles", "UploaderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.SharedFiles", "KlassID", "dbo.Klasses");
            DropIndex("dbo.SubmissionFiles", new[] { "KlassID" });
            DropIndex("dbo.SubmissionFiles", new[] { "UploaderID" });
            DropIndex("dbo.SharedFiles", new[] { "KlassID" });
            DropIndex("dbo.SharedFiles", new[] { "UploaderID" });
            DropTable("dbo.SubmissionFiles");
            DropTable("dbo.SharedFiles");
        }
    }
}
