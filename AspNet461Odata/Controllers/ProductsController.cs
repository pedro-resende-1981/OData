using AspNet461Odata.Data.Repositories;
using AspNet461Odata.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace AspNet461Odata.Controllers
{
    public class ProductsController : ODataController
    {
        private IBaseRepository<ProductVm> _productRepository;

        public ProductsController(IBaseRepository<ProductVm> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<ProductVm> Get()
        {
            return _productRepository.Get();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] Guid key)
        {
            return Ok(await _productRepository.GetByIDAsync(key));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(ProductVm productVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productRepository.CreateAsync(productVm);

            return Created(product);
        }


        [HttpPatch]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<ProductVm> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Updated(_productRepository.Patch(key, patch));
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] Guid key, ProductVm updatedProductVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != updatedProductVm.Guid)
            {
                return BadRequest();
            }

            return Updated(_productRepository.Update(key, updatedProductVm));
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            _productRepository.Delete(key);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}