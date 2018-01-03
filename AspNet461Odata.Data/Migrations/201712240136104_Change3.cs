namespace AspNet461Odata.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductLines", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductLines", new[] { "Product_Id" });
            RenameColumn(table: "dbo.ProductLines", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.ProductLines", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductLines", "ProductId");
            AddForeignKey("dbo.ProductLines", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductLines", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductLines", new[] { "ProductId" });
            AlterColumn("dbo.ProductLines", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.ProductLines", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.ProductLines", "Product_Id");
            AddForeignKey("dbo.ProductLines", "Product_Id", "dbo.Products", "Id");
        }
    }
}
