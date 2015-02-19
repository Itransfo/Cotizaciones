namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednewstepchangefix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StepChanges", "Step_StepId", "dbo.Steps");
            DropIndex("dbo.StepChanges", new[] { "Step_StepId" });
            DropColumn("dbo.StepChanges", "Step_StepId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StepChanges", "Step_StepId", c => c.Int());
            CreateIndex("dbo.StepChanges", "Step_StepId");
            AddForeignKey("dbo.StepChanges", "Step_StepId", "dbo.Steps", "StepId");
        }
    }
}
