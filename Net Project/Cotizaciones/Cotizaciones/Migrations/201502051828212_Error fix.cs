namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Errorfix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Extension", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Extension", c => c.Int(nullable: false));
        }
    }
}
