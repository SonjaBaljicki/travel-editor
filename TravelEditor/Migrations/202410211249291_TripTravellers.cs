namespace TravelEditor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TripTravellers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Travellers", "Trip_TripId", "dbo.Trips");
            DropIndex("dbo.Travellers", new[] { "Trip_TripId" });
            CreateTable(
                "dbo.TripTravellers",
                c => new
                    {
                        Trip_TripId = c.Int(nullable: false),
                        Traveller_TravellerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trip_TripId, t.Traveller_TravellerId })
                .ForeignKey("dbo.Trips", t => t.Trip_TripId, cascadeDelete: true)
                .ForeignKey("dbo.Travellers", t => t.Traveller_TravellerId, cascadeDelete: true)
                .Index(t => t.Trip_TripId)
                .Index(t => t.Traveller_TravellerId);
            
            DropColumn("dbo.Travellers", "Trip_TripId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Travellers", "Trip_TripId", c => c.Int());
            DropForeignKey("dbo.TripTravellers", "Traveller_TravellerId", "dbo.Travellers");
            DropForeignKey("dbo.TripTravellers", "Trip_TripId", "dbo.Trips");
            DropIndex("dbo.TripTravellers", new[] { "Traveller_TravellerId" });
            DropIndex("dbo.TripTravellers", new[] { "Trip_TripId" });
            DropTable("dbo.TripTravellers");
            CreateIndex("dbo.Travellers", "Trip_TripId");
            AddForeignKey("dbo.Travellers", "Trip_TripId", "dbo.Trips", "TripId");
        }
    }
}
