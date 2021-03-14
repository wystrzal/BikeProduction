using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductCommand command)
        {
            await mediator.Send(command);
            return Ok();

        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await mediator.Send(new DeleteProductCommand(id));
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] FilteringData filteringData)
        {
            return Ok(await mediator.Send(new GetProductsQuery(filteringData)));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return Ok(await mediator.Send(new GetProductQuery(id)));
        }

        [AllowAnonymous]
        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            return Ok(await mediator.Send(new GetBrandsQuery()));
        }

        [AllowAnonymous]
        [HttpGet("home/{homeProduct}")]
        public async Task<IActionResult> GetHomePageProducts(HomeProduct homeProduct)
        {
            return Ok(await mediator.Send(new GetHomeProductsQuery(homeProduct)));
        }

        [Authorize(Policy = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}