namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedstepchangestepfromnullableint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StepChanges", "PreviousStepId", c => c.Int(nullable: false));
            AlterColumn("dbo.StepChanges", "NextStepId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StepChanges", "NextStepId", c => c.Int());
            AlterColumn("dbo.StepChanges", "PreviousStepId", c => c.Int());
        }
    }
}
