using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAP.Data;
using SAP.Data.Models;
using SAP.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SAP.Services.Implementations
{
    public class CartService : ICartService
    {
        private ApplicationDbContext DbContext { get; }

        private UserManager<IdentityUser> UserManager { get; }

        private IHttpContextAccessor HttpContextAccessor { get; }

        private IMapper Mapper { get; }

        public CartService(ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            DbContext = dbContext;
            UserManager = userManager;
            HttpContextAccessor = httpContextAccessor;
            Mapper = mapper;
        }

        public async Task<CartViewModel> GetCurrentUserCartAsync()
        {
            var userId = UserManager.GetUserId(HttpContextAccessor.HttpContext.User);
            var cart = await DbContext.Carts
                .Include(c => c.ItemToCarts)
                    .ThenInclude(i => i.Item)
                .Include(c => c.ItemToCarts)
                    .ThenInclude(i => i.Attributes)
                .FirstOrDefaultAsync(x => x.User.Id == userId);

            if (cart != null)
            {
                var result = Mapper.Map<CartViewModel>(cart);
                result.Items.ForEach(x => result.TotalPrice += x.Attributes.Price * x.Quantity);
                return result;
            }

            return new CartViewModel();
        }

        public async Task AddToCartAsync(AddItemToCartViewModel newItem)
        {
            var item = await DbContext.Items.FirstOrDefaultAsync(x => x.Id == newItem.Id);
            if (item == null)
            {
                throw new Exception();
            }

            var cart = await GetCartAsync();
            cart.ItemToCarts.Add(new ItemToCart { Cart = cart, Item = item, Quantity = 1, AttributesId = newItem.AttributesId });
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> IsItemInCartAsync(Guid id)
        {
            var cart = await GetCartAsync();
            if(cart?.ItemToCarts?.Any(x => x.ItemId == id) == true)
            {
                return true;
            }

            return false;
        }

        public async Task ChangeQuantityAsync(Guid itemId, int dif)
        {
            var cart = await GetCartAsync();

            var itemInCart = cart.ItemToCarts.FirstOrDefault(x => x.ItemId == itemId);
            if (itemInCart == null)
            {
                throw new KeyNotFoundException();
            }

            itemInCart.Quantity += dif;
            if (itemInCart.Quantity <= 0)
            {
                DbContext.ItemToCarts.Remove(itemInCart);
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Guid itemId)
        {
            var cart = await GetCartAsync();
            var itemInCart = cart.ItemToCarts.FirstOrDefault(x => x.ItemId == itemId);
            if (itemInCart != null)
            {
                cart.ItemToCarts.Remove(itemInCart);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task ClearAsync()
        {
            var cart = await GetCartAsync();
            if (cart != null)
            {
                cart.ItemToCarts = new List<ItemToCart>();
                await DbContext.SaveChangesAsync();
            }
        }

        private async Task<Cart> GetCartAsync()
        {
            var user = await UserManager.GetUserAsync(HttpContextAccessor.HttpContext.User);
            var cart = await DbContext.Carts
                .Include(c => c.ItemToCarts)
                .FirstOrDefaultAsync(x => x.User.Id == user.Id);

            if (cart == null)
            {
                cart = new Cart { User = user };
                DbContext.Carts.Add(cart);
                cart.ItemToCarts = new List<ItemToCart>();
                await DbContext.SaveChangesAsync();
            }

            return cart;
        }
    }
}
