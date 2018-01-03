using System;
using System.Collections.Generic;

namespace AspNet461Odata.ViewModels
{
    public class SupplierVm
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductVm> Products { get; set; }
    }
}
