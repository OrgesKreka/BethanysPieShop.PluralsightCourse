using BethanysPieShop.API.Models;
using BethanysPieShop.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BethanysPieShop.API.Controllers
{
    [Route("api/[controller]/")]
    public class ShoppingCartController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ShoppingCartController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetItemsForUser(string userId)
        {
            var shoppingCart = _appDbContext.ShoppingCarts
                                             .Include(x => x.ShoppingCartItems)
                                             .ThenInclude(s => s.Pie)
                                             .FirstOrDefault(s => s.UserId == userId);

            return shoppingCart == null ? Ok(new ShoppingCart()) : Ok(shoppingCart);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserShoppingCartItem userShoppingCartItem)
        {
            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.UserId == userShoppingCartItem.UserId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { UserId = userShoppingCartItem.UserId };
                _appDbContext.ShoppingCarts.Add(shoppingCart);
                _appDbContext.SaveChanges();
            }

            var pie = _appDbContext.Pies.FirstOrDefault(p => p.PieId == userShoppingCartItem.ShoppingCartItem.PieId);

            var shoppingCartItem = new ShoppingCartItem
            {
                Pie = pie,
                PieId = userShoppingCartItem.ShoppingCartItem.Pie.PieId,
                Quantity = userShoppingCartItem.ShoppingCartItem.Quantity,
                ShoppingCartId = shoppingCart.ShoppingCartId
            };

            _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            _appDbContext.SaveChanges();

            return Ok(shoppingCartItem);
        }
    }
}
