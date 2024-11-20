namespace TravelEditor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attractions", "Destination_DestinationId", "dbo.Destinations");
            DropIndex("dbo.Attractions", new[] { "Destination_DestinationId" });
            AlterColumn("dbo.Attractions", "Destination_DestinationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attractions", "Destination_DestinationId");
            AddForeignKey("dbo.Attractions", "Destination_DestinationId", "dbo.Destinations", "DestinationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attractions", "Destination_DestinationId", "dbo.Destinations");
            DropIndex("dbo.Attractions", new[] { "Destination_DestinationId" });
            AlterColumn("dbo.Attractions", "Destination_DestinationId", c => c.Int());
            CreateIndex("dbo.Attractions", "Destination_DestinationId");
            AddForeignKey("dbo.Attractions", "Destination_DestinationId", "dbo.Destinations", "DestinationId");
        }
    }
}
