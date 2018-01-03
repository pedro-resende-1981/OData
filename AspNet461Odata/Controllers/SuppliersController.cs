using AspNet461Odata.Data.Models;
using AspNet461Odata.Data.Repositories;
using AspNet461Odata.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;

namespace AspNet461Odata.Controllers
{
    public class SuppliersController : ODataController
    {

        private SupplierRepository _supplierRepository = new SupplierRepository(new ProductsContext());

        [HttpGet]
        [EnableQuery]
        public IEnumerable<SupplierVm> Get()
        {
            return _supplierRepository.Get();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] Guid key)
        {
            return Ok(await _supplierRepository.GetByIDAsync(key));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(SupplierVm supplierVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var supplier = await _supplierRepository.CreateAsync(supplierVm);

            return Created(supplier);
        }


        [HttpPatch]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<SupplierVm> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Updated(_supplierRepository.Patch(key, patch));
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] Guid key, SupplierVm updatedSupplierVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != updatedSupplierVm.Guid)
            {
                return BadRequest();
            }

            return Updated(_supplierRepository.Update(key, updatedSupplierVm));
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            _supplierRepository.Delete(key);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}