namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Productprices : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProviderPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProviderPrice", c => c.Double());
        }
    }
}
