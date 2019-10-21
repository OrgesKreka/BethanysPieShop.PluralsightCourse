using BethanysPieShop.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BethanysPieShop.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public OrderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Order order)
        {
            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.UserId == order.UserId);

            var shoppingCartItemsToRemove = _appDbContext.ShoppingCartItems
                .Where(x => x.ShoppingCartId == shoppingCart.ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(shoppingCartItemsToRemove);
            _appDbContext.SaveChanges();

            return Ok(order);
        }
    }
}
