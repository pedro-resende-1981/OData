namespace AspNet461Odata.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductLines", "Order_Id", "dbo.Orders");
            DropIndex("dbo.ProductLines", new[] { "Order_Id" });
            DropColumn("dbo.ProductLines", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductLines", "Order_Id", c => c.Int());
            CreateIndex("dbo.ProductLines", "Order_Id");
            AddForeignKey("dbo.ProductLines", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
