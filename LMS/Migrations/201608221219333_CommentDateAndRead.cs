namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentDateAndRead : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Read", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Date");
            DropColumn("dbo.Comments", "Read");
        }
    }
}
