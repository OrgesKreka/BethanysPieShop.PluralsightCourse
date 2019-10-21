using BethanysPieShop.API.Models;

namespace BethanysPieShop.API.ViewModel
{
    public class UserShoppingCartItem
    {
        public string UserId { get; set; }

        public ShoppingCartItem ShoppingCartItem { get; set; }
    }
}
