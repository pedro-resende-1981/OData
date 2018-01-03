using AspNet461Odata.Data.Repositories;
using AspNet461Odata.ViewModels;
using StructureMap;

namespace AspNet461Odata.Ioc
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Scan(
                scan =>
                {
                    scan.WithDefaultConventions();
                });
            For<IBaseRepository<ProductVm>>().Use<ProductRepository>().ContainerScoped();
            For<IBaseRepository<SupplierVm>>().Use<SupplierRepository>().ContainerScoped();
            For<IBaseRepository<OrderVm>>().Use<OrderRepository>().ContainerScoped();
        }
    }
}