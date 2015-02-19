namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixednamestepsinstepchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StepChanges", "PreviousStepId", c => c.Int());
            AddColumn("dbo.StepChanges", "NextStepId", c => c.Int());
            DropColumn("dbo.StepChanges", "toStepId");
            DropColumn("dbo.StepChanges", "fromStepId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StepChanges", "fromStepId", c => c.Int());
            AddColumn("dbo.StepChanges", "toStepId", c => c.Int());
            DropColumn("dbo.StepChanges", "NextStepId");
            DropColumn("dbo.StepChanges", "PreviousStepId");
        }
    }
}
