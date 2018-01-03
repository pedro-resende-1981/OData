using System;
using System.Collections.Generic;

namespace AspNet461Odata.Data.Models
{
    public class Order
    {
        public Order()
        {
            Guid = Guid.NewGuid();
            OrderDate = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public Guid Guid { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public virtual ICollection<ProductLine> ProductLines { get; set; }

        public decimal Vat { get; set; }
    }
}
