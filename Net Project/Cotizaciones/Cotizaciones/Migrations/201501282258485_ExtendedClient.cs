namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendedClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "Company", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "City", c => c.String());
            AddColumn("dbo.Clients", "State", c => c.String());
            AddColumn("dbo.Clients", "Country", c => c.String());
            AddColumn("dbo.Clients", "Category", c => c.String());
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Clients", "Location");
            DropColumn("dbo.Clients", "Phone");
            DropColumn("dbo.Clients", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Email", c => c.String());
            AddColumn("dbo.Clients", "Phone", c => c.String());
            AddColumn("dbo.Clients", "Location", c => c.String());
            AlterColumn("dbo.Clients", "Name", c => c.String());
            DropColumn("dbo.Clients", "Category");
            DropColumn("dbo.Clients", "Country");
            DropColumn("dbo.Clients", "State");
            DropColumn("dbo.Clients", "City");
            DropColumn("dbo.Clients", "Company");
            DropColumn("dbo.Clients", "LastName");
        }
    }
}
