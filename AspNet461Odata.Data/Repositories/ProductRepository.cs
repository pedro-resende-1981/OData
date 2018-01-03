using System.Linq;
using AutoMapper;
using AspNet461Odata.ViewModels;
using AutoMapper.QueryableExtensions;
using System.Web.OData;
using System.Data.Entity;
using AspNet461Odata.Data.Models;
using System.Threading.Tasks;
using System;

namespace AspNet461Odata.Data.Repositories
{
    public class ProductRepository : IBaseRepository<ProductVm>
    {
        private ProductsContext _database;

        public ProductRepository(ProductsContext database)
        {
            _database = database;
        }

        public IQueryable<ProductVm> Get()
        {
            return _database.Products.ProjectTo<ProductVm>();
        }

        public async Task<ProductVm> GetByIDAsync(Guid key)
        {
            var product = await _database.Products
                .Include(p => p.Supplier)
                .SingleOrDefaultAsync(p => p.Guid == key);

            return Mapper.Map<ProductVm>(product);
        }

        public async Task<ProductVm> CreateAsync(ProductVm productVm)
        {
            productVm.Guid = Guid.NewGuid();
            var dbProduct = Mapper.Map<Product>(productVm);

            if (productVm.SupplierId.HasValue && productVm.SupplierId.Value != Guid.Empty)
            {
                var existingSupplier = _database.Suppliers.SingleOrDefault(supplier => supplier.Guid == productVm.SupplierId.Value);
                dbProduct.Supplier = existingSupplier;
            }
            else
            {
                dbProduct.Supplier = null;
            }

            _database.Products.Add(dbProduct);
            await _database.SaveChangesAsync();

            return Mapper.Map<ProductVm>(dbProduct);
        }

        public async Task<ProductVm> Update(Guid key, ProductVm updatedProductVm)
        {
            if(key != updatedProductVm.Guid)
            {
                return null;
            }

            var product = await _database.Products.SingleOrDefaultAsync(p => p.Guid == key);
            if(product == null)
            {
                return null;
            }

            Mapper.Map(updatedProductVm, product);
            _database.Products.Attach(product);
            _database.Entry(product).State = EntityState.Modified;
            _database.SaveChanges();

            return updatedProductVm;
        }

        public ProductVm Patch(Guid key, Delta<ProductVm> patch)
        {
            var product = _database.Products
                .Include(p => p.Supplier)
                .SingleOrDefault(p => p.Guid == key);


            if (product == null)
            {
                return null;
            }

            var productVm = Mapper.Map<ProductVm>(product);
            patch.Patch(productVm);
            Mapper.Map(productVm, product);

            if (productVm.SupplierId != product.Supplier?.Guid)
            {
                var supplier = productVm.SupplierId != null ? _database.Suppliers.SingleOrDefault(s => s.Guid == productVm.SupplierId) : null;
                product.Supplier = supplier;
            }

            _database.Entry(product).State = EntityState.Modified;
            _database.SaveChanges();

            return productVm;
        }

        public void Delete(Guid key)
        {
            var dbProduct = _database.Products.SingleOrDefault(p => p.Guid == key);
            if(dbProduct != null)
            {
                _database.Products.Remove(dbProduct);
                _database.SaveChanges();
            }
        }
    }
}
