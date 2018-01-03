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
    public class OrdersController : ODataController
    {
        private IBaseRepository<OrderVm> _orderRepository;

        public OrdersController(IBaseRepository<OrderVm> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<OrderVm> Get()
        {
            return _orderRepository.Get();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] Guid key)
        {
            return Ok(await _orderRepository.GetByIDAsync(key));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(OrderVm orderVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderRepository.CreateAsync(orderVm);

            return Created(order);
        }


        [HttpPatch]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<OrderVm> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Updated(_orderRepository.Patch(key, patch));
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] Guid key, OrderVm updatedOrderVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != updatedOrderVm.Reference)
            {
                return BadRequest();
            }

            return Updated(_orderRepository.Update(key, updatedOrderVm));
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            _orderRepository.Delete(key);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}