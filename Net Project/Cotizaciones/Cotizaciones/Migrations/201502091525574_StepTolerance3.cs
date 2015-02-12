namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StepTolerance3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StepTolerances",
                c => new
                    {
                        StepToleranceId = c.Int(nullable: false, identity: true),
                        Tolerance = c.Int(),
                        RemindEvery = c.Int(),
                        FromStep_StepId = c.Int(),
                        ToStep_StepId = c.Int(),
                    })
                .PrimaryKey(t => t.StepToleranceId)
                .ForeignKey("dbo.Steps", t => t.FromStep_StepId)
                .ForeignKey("dbo.Steps", t => t.ToStep_StepId)
                .Index(t => t.FromStep_StepId)
                .Index(t => t.ToStep_StepId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StepTolerances", "ToStep_StepId", "dbo.Steps");
            DropForeignKey("dbo.StepTolerances", "FromStep_StepId", "dbo.Steps");
            DropIndex("dbo.StepTolerances", new[] { "ToStep_StepId" });
            DropIndex("dbo.StepTolerances", new[] { "FromStep_StepId" });
            DropTable("dbo.StepTolerances");
        }
    }
}
