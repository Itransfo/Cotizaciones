namespace Cotizaciones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedordercommentstoorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderComments",
                c => new
                    {
                        OrderCommentId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        User = c.String(),
                        Date = c.DateTime(nullable: false),
                        Step_StepId = c.Int(),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderCommentId)
                .ForeignKey("dbo.Steps", t => t.Step_StepId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Step_StepId)
                .Index(t => t.Order_OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderComments", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderComments", "Step_StepId", "dbo.Steps");
            DropIndex("dbo.OrderComments", new[] { "Order_OrderId" });
            DropIndex("dbo.OrderComments", new[] { "Step_StepId" });
            DropTable("dbo.OrderComments");
        }
    }
}
