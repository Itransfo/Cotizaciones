namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Identifier = c.Int(),
                        Preproject = c.Int(),
                        RequestDescription = c.String(),
                        Client_ClientId = c.Int(),
                        Step_StepId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .ForeignKey("dbo.Steps", t => t.Step_StepId)
                .Index(t => t.Client_ClientId)
                .Index(t => t.Step_StepId);
            
            AddColumn("dbo.Products", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Products", "Order_OrderId");
            AddForeignKey("dbo.Products", "Order_OrderId", "dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Step_StepId", "dbo.Steps");
            DropForeignKey("dbo.Products", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.Products", new[] { "Order_OrderId" });
            DropIndex("dbo.Orders", new[] { "Step_StepId" });
            DropIndex("dbo.Orders", new[] { "Client_ClientId" });
            DropColumn("dbo.Products", "Order_OrderId");
            DropTable("dbo.Orders");
        }
    }
}
