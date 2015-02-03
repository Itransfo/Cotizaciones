namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stepresponsibilities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Steps", "Responsible", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Steps", "Responsible");
        }
    }
}
