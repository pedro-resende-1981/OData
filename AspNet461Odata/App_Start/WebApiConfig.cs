using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.Http;
using AutoMapper;
using AspNet461Odata.Data.Models;
using AspNet461Odata.ViewModels;
using Microsoft.OData.Edm;

namespace AspNet461Odata
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: GetModel());

            config
                .Count()
                .Filter()
                .OrderBy()
                .Expand()
                .Select()
                .MaxTop(null);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductVm>()
                    .ForMember(dest => dest.Guid, opt => opt.MapFrom(product => product.Guid))
                    .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(product => product.Supplier.Guid))
                    .ReverseMap();

                cfg.CreateMap<Supplier, SupplierVm>()
                    .ForMember(dest => dest.Guid, opt => opt.MapFrom(supplier => supplier.Guid))
                    .ReverseMap();

                cfg.CreateMap<ProductVm, Product>()
                    .ForMember(dest => dest.Supplier, opt => opt.UseDestinationValue());

                cfg.CreateMap<ProductLine, ProductLineVm>()
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(productLine => productLine.Product.Guid))
                    .ReverseMap();

                cfg.CreateMap<Order, OrderVm>()
                    .ForMember(dest => dest.Reference, opt => opt.MapFrom(order => order.Guid))
                    .ReverseMap();

                cfg.CreateMap<OrderVm, Order>()
                    .ForMember(dest => dest.ProductLines, opt => opt.Ignore());
            });
        }

        private static IEdmModel GetModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<ProductVm>("Products").EntityType.HasKey(p => p.Guid);
            builder.EntitySet<SupplierVm>("Suppliers").EntityType.HasKey(s => s.Guid);
            builder.EntitySet<OrderVm>("Orders").EntityType.HasKey(o => o.Reference);

            return builder.GetEdmModel();
        }
    }
}
