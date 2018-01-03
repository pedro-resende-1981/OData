namespace AspNet461Odata.Data.Migrations
{
    using AspNet461Odata.Data.Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNet461Odata.Data.Models.ProductsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AspNet461Odata.Data.Models.ProductsContext context)
        {
            //  This method will be called after migrating to the latest version.

            var suppliers = new[]
            {
                new Supplier { Guid = Guid.NewGuid(), Name = "Supplier 1" },
                new Supplier { Guid = Guid.NewGuid(), Name = "Supplier 2" },
                new Supplier { Guid = Guid.NewGuid(), Name = "Supplier 3" }
            };

            var products = new[]
           {
                new Product { Guid = Guid.NewGuid(), Name = "Product 1", Category = "Category 1", Price = 100, Supplier = suppliers[0] },
                new Product { Guid = Guid.NewGuid(), Name = "Product 2", Category = "Category 1", Price = 80, Supplier = suppliers[0] },
                new Product { Guid = Guid.NewGuid(), Name = "Product 3", Category = "Category 2", Price = 999, Supplier = suppliers[1] },
                new Product { Guid = Guid.NewGuid(), Name = "Product 4", Category = "Category 2", Price = 1099, Supplier = suppliers[1] },
                new Product { Guid = Guid.NewGuid(), Name = "Product 5", Category = "Category 3", Price = 49.90m, Supplier = suppliers[2] },
                new Product { Guid = Guid.NewGuid(), Name = "Product 6", Category = "Category 3", Price = 499, Supplier = suppliers[2] }
            };

            var orders = new[]
            {
                new Order
                {
                    CustomerAddress = "Test Street 1096",
                    CustomerName = "John Doe",
                    ProductLines = new[]
                    {
                        new ProductLine
                        {
                            Product = products[0],
                            Price = 100,
                            Quantity = 2
                        }
                    }
                }
            };

            context.Products.AddOrUpdate(products);
            context.Orders.AddOrUpdate(orders);
        }
    }
}
