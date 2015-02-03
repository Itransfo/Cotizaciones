namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stepupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Steps", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Steps", "Order");
        }
    }
}
