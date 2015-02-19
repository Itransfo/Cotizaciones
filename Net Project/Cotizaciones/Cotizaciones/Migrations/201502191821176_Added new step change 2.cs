namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednewstepchange2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StepChanges",
                c => new
                    {
                        StepChangeId = c.Int(nullable: false, identity: true),
                        DateChanged = c.DateTime(nullable: false),
                        toStepId = c.Int(),
                        fromStepId = c.Int(),
                        User = c.String(),
                        Order_OrderId = c.Int(),
                        Step_StepId = c.Int(),
                    })
                .PrimaryKey(t => t.StepChangeId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .ForeignKey("dbo.Steps", t => t.Step_StepId)
                .Index(t => t.Order_OrderId)
                .Index(t => t.Step_StepId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StepChanges", "Step_StepId", "dbo.Steps");
            DropForeignKey("dbo.StepChanges", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.StepChanges", new[] { "Step_StepId" });
            DropIndex("dbo.StepChanges", new[] { "Order_OrderId" });
            DropTable("dbo.StepChanges");
        }
    }
}
