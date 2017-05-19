namespace SiteSerials.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Four : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Serials", "User_Id", "dbo.Users");
            DropIndex("dbo.Serials", new[] { "User_Id" });
            CreateTable(
                "dbo.UserSerials",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Serial_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Serial_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Serials", t => t.Serial_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Serial_Id);
            
            DropColumn("dbo.Serials", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Serials", "User_Id", c => c.Int());
            DropForeignKey("dbo.UserSerials", "Serial_Id", "dbo.Serials");
            DropForeignKey("dbo.UserSerials", "User_Id", "dbo.Users");
            DropIndex("dbo.UserSerials", new[] { "Serial_Id" });
            DropIndex("dbo.UserSerials", new[] { "User_Id" });
            DropTable("dbo.UserSerials");
            CreateIndex("dbo.Serials", "User_Id");
            AddForeignKey("dbo.Serials", "User_Id", "dbo.Users", "Id");
        }
    }
}
