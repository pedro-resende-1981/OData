namespace AspNet461Odata.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductLines", "Order_Id", "dbo.Orders");
            DropIndex("dbo.ProductLines", new[] { "Order_Id" });
            RenameColumn(table: "dbo.ProductLines", name: "Order_Id", newName: "OrderId");
            AlterColumn("dbo.ProductLines", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductLines", "OrderId");
            AddForeignKey("dbo.ProductLines", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductLines", "OrderId", "dbo.Orders");
            DropIndex("dbo.ProductLines", new[] { "OrderId" });
            AlterColumn("dbo.ProductLines", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.ProductLines", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.ProductLines", "Order_Id");
            AddForeignKey("dbo.ProductLines", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
