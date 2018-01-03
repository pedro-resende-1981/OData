using System;

namespace AspNet461Odata.ViewModels
{
    public class ProductLineVm
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
