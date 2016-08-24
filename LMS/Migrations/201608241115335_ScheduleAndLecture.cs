namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleAndLecture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KlassSchedules",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasses", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Description = c.String(),
                        Location = c.String(),
                        ScheduleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KlassSchedules", t => t.ScheduleID, cascadeDelete: true)
                .Index(t => t.ScheduleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "ScheduleID", "dbo.KlassSchedules");
            DropForeignKey("dbo.KlassSchedules", "ID", "dbo.Klasses");
            DropIndex("dbo.Lectures", new[] { "ScheduleID" });
            DropIndex("dbo.KlassSchedules", new[] { "ID" });
            DropTable("dbo.Lectures");
            DropTable("dbo.KlassSchedules");
        }
    }
}
