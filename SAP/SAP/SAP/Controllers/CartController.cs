using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAP.Services;
using SAP.ViewModels.Cart;

namespace SAP.Controllers
{
    [Authorize]
    [Route("cart")]
    public class CartController : Controller
    {
        private ICartService CartService { get; }

        public CartController(ICartService cartService)
        {
            CartService = cartService;
        }

        [Route("")]
        // GET: Cart
        public async Task<ActionResult> Index()
        {
            var cart = await CartService.GetCurrentUserCartAsync();
            return View(cart);
        }

        [Route("add_item")]
        [HttpPost]
        public async Task<ActionResult> AddItem(AddItemToCartViewModel item)
        {
            if (!await CartService.IsItemInCartAsync(item.Id))
            {
                await CartService.AddToCartAsync(item);
            }

            return RedirectToAction("Item", "Catalogue", new { item.Id });
        }

        [Route("change_quantity/{itemId}/{dif}")]
        public async Task<ActionResult> ChangeQuantity(Guid itemId, int dif)
        {
            await CartService.ChangeQuantityAsync(itemId, dif);

            return RedirectToAction("Index");
        }

        [Route("remove/{itemId}")]
        public async Task<ActionResult> Remove(Guid itemId)
        {
            await CartService.RemoveItemAsync(itemId);

            return RedirectToAction("Index");
        }

        [Route("clear")]
        public async Task<ActionResult> Clear()
        {
            await CartService.ClearAsync();

            return RedirectToAction("Index");
        }
    }
}