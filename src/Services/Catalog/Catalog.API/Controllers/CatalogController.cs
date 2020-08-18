using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductCommand command)
        {
            try
            {
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await mediator.Send(new DeleteProductCommand(id));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery]FilteringData filteringData)
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
    }
}