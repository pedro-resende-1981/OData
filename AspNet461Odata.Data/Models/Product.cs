using System;

namespace AspNet461Odata.Data.Models
{
    public class Product
    {
        public int Id { get; private set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}