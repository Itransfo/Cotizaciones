namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StepStakeholders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StepStakeholders",
                c => new
                    {
                        StepStakeholderId = c.Int(nullable: false, identity: true),
                        Stakeholder = c.String(),
                        Step_StepId = c.Int(),
                    })
                .PrimaryKey(t => t.StepStakeholderId)
                .ForeignKey("dbo.Steps", t => t.Step_StepId)
                .Index(t => t.Step_StepId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StepStakeholders", "Step_StepId", "dbo.Steps");
            DropIndex("dbo.StepStakeholders", new[] { "Step_StepId" });
            DropTable("dbo.StepStakeholders");
        }
    }
}
