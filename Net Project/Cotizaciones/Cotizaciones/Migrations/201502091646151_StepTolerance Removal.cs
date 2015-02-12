namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StepToleranceRemoval : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StepTolerances", "FromStep_StepId", "dbo.Steps");
            DropForeignKey("dbo.StepTolerances", "ToStep_StepId", "dbo.Steps");
            DropIndex("dbo.StepTolerances", new[] { "FromStep_StepId" });
            DropIndex("dbo.StepTolerances", new[] { "ToStep_StepId" });
            AddColumn("dbo.Steps", "Tolerance", c => c.Int(nullable: false));
            AddColumn("dbo.Steps", "Reminder", c => c.Int(nullable: false));
            DropTable("dbo.StepTolerances");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StepTolerances",
                c => new
                    {
                        StepToleranceID = c.Int(nullable: false, identity: true),
                        FromStepID = c.Int(nullable: false),
                        ToStepID = c.Int(nullable: false),
                        Tolerance = c.Int(),
                        RemindEvery = c.Int(),
                        FromStep_StepId = c.Int(),
                        ToStep_StepId = c.Int(),
                    })
                .PrimaryKey(t => t.StepToleranceID);
            
            DropColumn("dbo.Steps", "Reminder");
            DropColumn("dbo.Steps", "Tolerance");
            CreateIndex("dbo.StepTolerances", "ToStep_StepId");
            CreateIndex("dbo.StepTolerances", "FromStep_StepId");
            AddForeignKey("dbo.StepTolerances", "ToStep_StepId", "dbo.Steps", "StepId");
            AddForeignKey("dbo.StepTolerances", "FromStep_StepId", "dbo.Steps", "StepId");
        }
    }
}
