namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProviderId = c.String(),
                        Provider = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        ProductFamily = c.String(),
                        ProviderPrice = c.String(),
                        Currency = c.String(),
                        AdditionalData = c.String(),
                        ImageURL = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Products", new[] { "Product_ProductId" });
            DropTable("dbo.Products");
        }
    }
}
