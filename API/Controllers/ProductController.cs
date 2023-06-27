using System.Security.Claims;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<Product>> Create(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var result = await _uow.Products.CreateProduct(product);

            _uow.Complete();

            return Ok(result);
        }
    }
}
