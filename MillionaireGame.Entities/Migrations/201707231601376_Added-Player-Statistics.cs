namespace MillionaireGame.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPlayerStatistics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerStatistics", "ResultDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.PlayerStatistics", "ResulDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerStatistics", "ResulDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.PlayerStatistics", "ResultDateTime");
        }
    }
}
