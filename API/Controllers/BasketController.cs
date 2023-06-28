using System.Threading.Tasks;
using API.Extensions;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class BasketController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public BasketController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var userId = HttpContext.User.GetUserId();
            var basket = await _uow.Baskets.RetrieveBasket(userId);

            if (basket == null)
                return NotFound();

            return basket.MapBasketToDto();
        }

        [HttpPost("{productId}/{qantity}")]
        public async Task<ActionResult> AddToBasket(int productId, int qantity)
        {
            if (productId <= 0 && qantity <= 0)
                return BadRequest("Bad Inputs");

            var userId = HttpContext.User.GetUserId();

            var basket = await _uow.Baskets.RetrieveBasket(userId);

            if (basket == null)
                basket = await _uow.Baskets.CreateBasket(userId);

            var product = await _uow.Products.GetSingleProduct(productId);

            if (product == null)
                return BadRequest(new ProblemDetails { Title = "Product not found" });

            basket.AddItem(product, qantity);

            var result = _uow.Complete() > 0;

            if (result)
                return Ok(basket.MapBasketToDto());

            return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
        }

        [HttpDelete("{productId}/{qantity}")]
        public async Task<ActionResult> RemoveFromBasket(int productId, int quantity = 1)
        {
            var userId = HttpContext.User.GetUserId();
            var basket = await _uow.Baskets.RetrieveBasket(userId);

            if (basket == null)
                return NotFound();

            basket.RemoveItem(productId, quantity);

            var result = _uow.Complete() > 0;

            if (result)
                return Ok();

            return BadRequest(
                new ProblemDetails { Title = "Problem removing item from the basket" }
            );
        }
    }
}
