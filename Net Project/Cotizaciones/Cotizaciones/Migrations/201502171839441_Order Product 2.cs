namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderProduct2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderProductId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(),
                        Order_OrderId = c.Int(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderProductId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Order_OrderId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "Order_OrderId" });
            DropTable("dbo.OrderProducts");
        }
    }
}
