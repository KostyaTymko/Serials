namespace SiteSerials.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class One : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Serials", "User_Id", c => c.Int());
            CreateIndex("dbo.Serials", "User_Id");
            AddForeignKey("dbo.Serials", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Serials", "User_Id", "dbo.Users");
            DropIndex("dbo.Serials", new[] { "User_Id" });
            DropColumn("dbo.Serials", "User_Id");
            DropTable("dbo.Users");
        }
    }
}
