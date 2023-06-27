using System.Security.Claims;
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

        public ProductController(IUnitOfWork uow)
        {
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
        public ActionResult Create()
        {
            return Ok();
        }
    }
}
