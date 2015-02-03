namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        StepId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StepId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Steps");
        }
    }
}
