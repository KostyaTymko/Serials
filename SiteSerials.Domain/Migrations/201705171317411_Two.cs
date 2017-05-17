namespace SiteSerials.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Two : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Serials", "Serial_title", c => c.String(nullable: false));
            AlterColumn("dbo.Serials", "SerialDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Serials", "Category", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Serials", "Category", c => c.String());
            AlterColumn("dbo.Serials", "SerialDescription", c => c.String());
            AlterColumn("dbo.Serials", "Serial_title", c => c.String());
        }
    }
}
