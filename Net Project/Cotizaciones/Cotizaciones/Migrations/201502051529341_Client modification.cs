namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clientmodification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Address", c => c.String());
            AddColumn("dbo.Clients", "Phone", c => c.String());
            AddColumn("dbo.Clients", "Extension", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "CellPhone", c => c.String());
            AddColumn("dbo.Clients", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Email");
            DropColumn("dbo.Clients", "CellPhone");
            DropColumn("dbo.Clients", "Extension");
            DropColumn("dbo.Clients", "Phone");
            DropColumn("dbo.Clients", "Address");
        }
    }
}
