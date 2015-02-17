namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orderdatefix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Identifier", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Identifier", c => c.Int());
        }
    }
}
