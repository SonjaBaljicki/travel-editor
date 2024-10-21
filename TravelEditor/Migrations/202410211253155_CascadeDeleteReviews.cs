namespace TravelEditor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDeleteReviews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Trip_TripId", "dbo.Trips");
            DropIndex("dbo.Reviews", new[] { "Trip_TripId" });
            AlterColumn("dbo.Reviews", "Trip_TripId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "Trip_TripId");
            AddForeignKey("dbo.Reviews", "Trip_TripId", "dbo.Trips", "TripId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Trip_TripId", "dbo.Trips");
            DropIndex("dbo.Reviews", new[] { "Trip_TripId" });
            AlterColumn("dbo.Reviews", "Trip_TripId", c => c.Int());
            CreateIndex("dbo.Reviews", "Trip_TripId");
            AddForeignKey("dbo.Reviews", "Trip_TripId", "dbo.Trips", "TripId");
        }
    }
}
