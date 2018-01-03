using System.Linq;
using AutoMapper;
using AspNet461Odata.ViewModels;
using AutoMapper.QueryableExtensions;
using System.Web.OData;
using System.Data.Entity;
using AspNet461Odata.Data.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace AspNet461Odata.Data.Repositories
{
    public class OrderRepository : IBaseRepository<OrderVm>
    {
        private ProductsContext _database;

        public OrderRepository(ProductsContext database)
        {
            _database = database;
        }

        public IQueryable<OrderVm> Get()
        {
            return _database.Orders.ProjectTo<OrderVm>();
        }

        public async Task<OrderVm> GetByIDAsync(Guid key)
        {
            var order = await _database.Orders
                .Include(o => o.ProductLines)
                .SingleOrDefaultAsync(o => o.Guid == key);

            return Mapper.Map<OrderVm>(order);
        }

        public async Task<OrderVm> CreateAsync(OrderVm orderVm)
        {
            orderVm.Reference = Guid.NewGuid();
            var dbOrder = Mapper.Map<Order>(orderVm);

            var orderProducts = orderVm.ProductLines.Select(p => p.ProductId);
            var products = _database.Products.Where(p => orderProducts.Contains(p.Guid));

            var productLines = new List<ProductLine>();
            foreach (var p in products)
            {
                var newProductLine = new ProductLine
                {
                    Product = p,
                    Price = p.Price,
                    Quantity = orderVm.ProductLines.Single(prd => prd.ProductId == p.Guid).Quantity
                };

                productLines.Add(newProductLine);
            }
            dbOrder.ProductLines = productLines;

            _database.Orders.Add(dbOrder);
            await _database.SaveChangesAsync();

            return Mapper.Map<OrderVm>(dbOrder);
        }

        public async Task<OrderVm> Update(Guid key, OrderVm updatedOrderVm)
        {
            if (key != updatedOrderVm.Reference)
            {
                return null;
            }

            var order = _database.Orders
                .Include(o => o.ProductLines)
                .SingleOrDefault(o => o.Guid == key);

            if (order == null)
            {
                return null;
            }

            _database.ProductLines.RemoveRange(order.ProductLines);

            Mapper.Map(updatedOrderVm, order);

            var newProductList = new List<ProductLine>();
            foreach (var newProduct in updatedOrderVm.ProductLines)
            {
                var p = new ProductLine
                {
                    Price = newProduct.Price,
                    Quantity = newProduct.Quantity,
                    ProductId = _database.Products.Single(x => x.Guid == newProduct.ProductId).Id,
                };

                if (p.ProductId != 0)
                {
                    newProductList.Add(p);
                }
            }
            order.ProductLines = newProductList;

            _database.Orders.Attach(order);
            _database.Entry(order).State = EntityState.Modified;
            await _database.SaveChangesAsync();

            return updatedOrderVm;
        }

        public OrderVm Patch(Guid key, Delta<OrderVm> patch)
        {
            var order = _database.Orders
                .Include(o => o.ProductLines)
                .SingleOrDefault(o => o.Guid == key);

            if (order == null)
            {
                return null;
            }

            var orderVm = Mapper.Map<OrderVm>(order);
            patch.Patch(orderVm);
            Mapper.Map(orderVm, order);

            var changedPropertyNames = patch.GetChangedPropertyNames();
            if (changedPropertyNames.Contains("ProductLines"))
            {
                _database.ProductLines.RemoveRange(order.ProductLines);
                var newProductList = new List<ProductLine>();
                foreach (var newProduct in orderVm.ProductLines)
                {
                    var p = new ProductLine
                    {
                        Price = newProduct.Price,
                        Quantity = newProduct.Quantity,
                        ProductId = _database.Products.Single(x => x.Guid == newProduct.ProductId).Id,
                    };

                    if (p.ProductId != 0)
                    {
                        newProductList.Add(p);
                    }
                }
                order.ProductLines = newProductList;
            }

            _database.Entry(order).State = EntityState.Modified;
            _database.SaveChanges();

            return orderVm;
        }

        public void Delete(Guid key)
        {
            var dbOrder = _database.Orders.SingleOrDefault(p => p.Guid == key);
            if(dbOrder != null)
            {
                _database.Orders.Remove(dbOrder);
                _database.SaveChanges();
            }
        }
    }
}
