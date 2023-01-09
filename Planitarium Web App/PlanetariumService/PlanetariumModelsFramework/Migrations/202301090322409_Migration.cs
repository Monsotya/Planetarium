namespace PlanetariumModelsFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HallName = c.String(),
                        Capacity = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfEvent = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PerformanceId = c.Int(nullable: false),
                        HallId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Halls", t => t.HallId, cascadeDelete: true)
                .ForeignKey("dbo.Performances", t => t.PerformanceId, cascadeDelete: true)
                .Index(t => t.PerformanceId)
                .Index(t => t.HallId);
            
            CreateTable(
                "dbo.Performances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        EventDescription = c.String(),
                        Duration = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketStatus = c.String(),
                        Place = c.Byte(nullable: false),
                        TierId = c.Int(nullable: false),
                        PosterId = c.Int(nullable: false),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Posters", t => t.PosterId, cascadeDelete: true)
                .ForeignKey("dbo.Tiers", t => t.TierId, cascadeDelete: true)
                .Index(t => t.TierId)
                .Index(t => t.PosterId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfOrder = c.DateTime(nullable: false),
                        ClientName = c.String(),
                        ClientSurname = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tiers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TierName = c.String(),
                        Surcharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        UserPassword = c.String(),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TierId", "dbo.Tiers");
            DropForeignKey("dbo.Tickets", "PosterId", "dbo.Posters");
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Posters", "PerformanceId", "dbo.Performances");
            DropForeignKey("dbo.Posters", "HallId", "dbo.Halls");
            DropIndex("dbo.Tickets", new[] { "OrderId" });
            DropIndex("dbo.Tickets", new[] { "PosterId" });
            DropIndex("dbo.Tickets", new[] { "TierId" });
            DropIndex("dbo.Posters", new[] { "HallId" });
            DropIndex("dbo.Posters", new[] { "PerformanceId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tiers");
            DropTable("dbo.Orders");
            DropTable("dbo.Tickets");
            DropTable("dbo.Performances");
            DropTable("dbo.Posters");
            DropTable("dbo.Halls");
        }
    }
}
