namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedorderidentifiertostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Identifier", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Identifier", c => c.Int(nullable: false));
        }
    }
}
