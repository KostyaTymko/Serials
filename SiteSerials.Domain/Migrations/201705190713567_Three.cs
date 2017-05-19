namespace SiteSerials.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Serials", "ImageData", c => c.Binary());
            AddColumn("dbo.Serials", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Serials", "ImageMimeType");
            DropColumn("dbo.Serials", "ImageData");
        }
    }
}
