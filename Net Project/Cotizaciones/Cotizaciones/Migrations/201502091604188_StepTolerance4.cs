namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StepTolerance4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StepTolerances", "FromStepID", c => c.Int(nullable: false));
            AddColumn("dbo.StepTolerances", "ToStepID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StepTolerances", "ToStepID");
            DropColumn("dbo.StepTolerances", "FromStepID");
        }
    }
}
