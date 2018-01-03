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
    public class SupplierRepository : IBaseRepository<SupplierVm>
    {
        private ProductsContext _database;

        public SupplierRepository(ProductsContext database)
        {
            _database = database;
        }

        public IQueryable<SupplierVm> Get()
        {
            return _database.Suppliers.Include(s => s.Products).ProjectTo<SupplierVm>();
        }

        public async Task<SupplierVm> GetByIDAsync(Guid id)
        {
            var supplier = await _database.Suppliers.SingleOrDefaultAsync(s => s.Guid == id);

            return Mapper.Map<SupplierVm>(supplier);
        }

        public async Task<SupplierVm> CreateAsync(SupplierVm supplierVm)
        {
            supplierVm.Guid = Guid.NewGuid();
            supplierVm.Products = new List<ProductVm>();
            var dbSupplier = Mapper.Map<Supplier>(supplierVm);
            _database.Suppliers.Add(dbSupplier);
            await _database.SaveChangesAsync();

            return Mapper.Map<SupplierVm>(dbSupplier);
        }

        public async Task<SupplierVm> Update(Guid id, SupplierVm updateSupplierVm)
        {
            if(id != updateSupplierVm.Guid)
            {
                return null;
            }

            var supplier = _database.Suppliers.SingleOrDefault(s => s.Guid == id);
            if(supplier == null)
            {
                return null;
            }

            Mapper.Map(updateSupplierVm, supplier);
            _database.Suppliers.Attach(supplier);
            _database.Entry(supplier).State = EntityState.Modified;
            await _database.SaveChangesAsync();

            return updateSupplierVm;
        }

        public SupplierVm Patch(Guid id, Delta<SupplierVm> patch)
        {
            var supplier = _database.Suppliers.SingleOrDefault(s => s.Guid == id);
            if (supplier == null)
            {
                return null;
            }

            var supplierVm = Mapper.Map<SupplierVm>(supplier);
            patch.Patch(supplierVm);

            Mapper.Map(supplierVm, supplier);
            _database.Entry(supplier).State = EntityState.Modified;
            _database.SaveChanges();

            return supplierVm;
        }

        public void Delete(Guid id)
        {
            var dbSupplier = _database.Suppliers.SingleOrDefault(s =>s.Guid == id);
            if(dbSupplier != null)
            {
                _database.Suppliers.Remove(dbSupplier);
                _database.SaveChanges();
            }
        }
    }
}
