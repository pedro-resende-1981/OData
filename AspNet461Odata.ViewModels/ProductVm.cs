using System;

namespace AspNet461Odata.ViewModels
{
    public class ProductVm
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public Guid? SupplierId { get; set; }
    }
}