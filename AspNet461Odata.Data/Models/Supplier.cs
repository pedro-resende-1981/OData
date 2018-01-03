using System;
using System.Collections.Generic;

namespace AspNet461Odata.Data.Models
{
    public class Supplier
    {
        public int Id { get; private set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}