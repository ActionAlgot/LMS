namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserKlasses", newName: "KlassApplicationUsers");
            DropPrimaryKey("dbo.KlassApplicationUsers");
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CommenterID = c.String(maxLength: 128),
                        SubmissionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CommenterID)
                .ForeignKey("dbo.SubmissionFiles", t => t.SubmissionID, cascadeDelete: true)
                .Index(t => t.CommenterID)
                .Index(t => t.SubmissionID);
            
            AddPrimaryKey("dbo.KlassApplicationUsers", new[] { "Klass_ID", "ApplicationUser_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "SubmissionID", "dbo.SubmissionFiles");
            DropForeignKey("dbo.Comments", "CommenterID", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "SubmissionID" });
            DropIndex("dbo.Comments", new[] { "CommenterID" });
            DropPrimaryKey("dbo.KlassApplicationUsers");
            DropTable("dbo.Comments");
            AddPrimaryKey("dbo.KlassApplicationUsers", new[] { "ApplicationUser_Id", "Klass_ID" });
            RenameTable(name: "dbo.KlassApplicationUsers", newName: "ApplicationUserKlasses");
        }
    }
}
