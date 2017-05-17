namespace SiteSerials.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Serials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Serial_title = c.String(),
                        SerialDescription = c.String(),
                        Category = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Genre_title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialId = c.Int(nullable: false),
                        Season_title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Serials", t => t.SerialId, cascadeDelete: true)
                .Index(t => t.SerialId);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeasonId = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seasons", t => t.SeasonId, cascadeDelete: true)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.GenreSerials",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Serial_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Serial_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Serials", t => t.Serial_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Serial_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Series", "SeasonId", "dbo.Seasons");
            DropForeignKey("dbo.Seasons", "SerialId", "dbo.Serials");
            DropForeignKey("dbo.GenreSerials", "Serial_Id", "dbo.Serials");
            DropForeignKey("dbo.GenreSerials", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.GenreSerials", new[] { "Serial_Id" });
            DropIndex("dbo.GenreSerials", new[] { "Genre_Id" });
            DropIndex("dbo.Series", new[] { "SeasonId" });
            DropIndex("dbo.Seasons", new[] { "SerialId" });
            DropTable("dbo.GenreSerials");
            DropTable("dbo.Series");
            DropTable("dbo.Seasons");
            DropTable("dbo.Genres");
            DropTable("dbo.Serials");
        }
    }
}
