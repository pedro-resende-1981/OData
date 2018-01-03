namespace AspNet461Odata.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductLines", "Order_Id", c => c.Int());
            CreateIndex("dbo.ProductLines", "Order_Id");
            AddForeignKey("dbo.ProductLines", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductLines", "Order_Id", "dbo.Orders");
            DropIndex("dbo.ProductLines", new[] { "Order_Id" });
            DropColumn("dbo.ProductLines", "Order_Id");
        }
    }
}
