using System;
using System.Collections.Generic;

namespace AspNet461Odata.ViewModels
{
    public class OrderVm
    {
        public Guid Reference { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public ICollection<ProductLineVm> ProductLines { get; set; }

        public decimal Vat { get; set; }
    }
}
