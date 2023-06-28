using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;

            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var result = await _uow.Products.GetProducts();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleProduct(int id)
        {
            var result = await _uow.Products.GetSingleProduct(id);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var result = await _uow.Products.CreateProduct(product);

            _uow.Complete();

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct(UpdateProductDto productDto)
        {
            var product = await _uow.Products.GetSingleProduct(productDto.Id);

            if (product == null)
                return NotFound();

            _mapper.Map(productDto, product);

            var result = _uow.Complete() > 0;

            if (result)
                return Ok(product);

            return BadRequest(new ProblemDetails { Title = "Problem updating product" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _uow.Products.GetSingleProduct(id);

            if (product == null)
                return NotFound();

            _uow.Products.RemoveProduct(product);

            var result = _uow.Complete() > 0;

            if (result)
                return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
        }
    }
}
