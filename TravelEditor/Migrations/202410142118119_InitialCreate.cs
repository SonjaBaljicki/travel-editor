namespace TravelEditor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attractions",
                c => new
                    {
                        AttractionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Location = c.String(),
                        Destination_DestinationId = c.Int(),
                    })
                .PrimaryKey(t => t.AttractionId)
                .ForeignKey("dbo.Destinations", t => t.Destination_DestinationId)
                .Index(t => t.Destination_DestinationId);
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        DestinationId = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Country = c.String(),
                        Description = c.String(),
                        Climate = c.String(),
                    })
                .PrimaryKey(t => t.DestinationId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TravellerId = c.Int(nullable: false),
                        Trip_TripId = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Travellers", t => t.TravellerId, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.Trip_TripId)
                .Index(t => t.TravellerId)
                .Index(t => t.Trip_TripId);
            
            CreateTable(
                "dbo.Travellers",
                c => new
                    {
                        TravellerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Age = c.Int(nullable: false),
                        Trip_TripId = c.Int(),
                    })
                .PrimaryKey(t => t.TravellerId)
                .ForeignKey("dbo.Trips", t => t.Trip_TripId)
                .Index(t => t.Trip_TripId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        TripId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Destination_DestinationId = c.Int(),
                    })
                .PrimaryKey(t => t.TripId)
                .ForeignKey("dbo.Destinations", t => t.Destination_DestinationId)
                .Index(t => t.Destination_DestinationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travellers", "Trip_TripId", "dbo.Trips");
            DropForeignKey("dbo.Reviews", "Trip_TripId", "dbo.Trips");
            DropForeignKey("dbo.Trips", "Destination_DestinationId", "dbo.Destinations");
            DropForeignKey("dbo.Reviews", "TravellerId", "dbo.Travellers");
            DropForeignKey("dbo.Attractions", "Destination_DestinationId", "dbo.Destinations");
            DropIndex("dbo.Trips", new[] { "Destination_DestinationId" });
            DropIndex("dbo.Travellers", new[] { "Trip_TripId" });
            DropIndex("dbo.Reviews", new[] { "Trip_TripId" });
            DropIndex("dbo.Reviews", new[] { "TravellerId" });
            DropIndex("dbo.Attractions", new[] { "Destination_DestinationId" });
            DropTable("dbo.Trips");
            DropTable("dbo.Travellers");
            DropTable("dbo.Reviews");
            DropTable("dbo.Destinations");
            DropTable("dbo.Attractions");
        }
    }
}
