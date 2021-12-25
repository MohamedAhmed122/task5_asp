using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAP.Data;
using SAP.Data.Models;
using SAP.ViewModels.Orders;

namespace SAP.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext DbContext { get; }

        private UserManager<IdentityUser> UserManager { get; }

        private IHttpContextAccessor HttpContextAccessor { get; }

        private IMapper Mapper { get; }

        private ICartService CartService { get; }

        public OrderService(ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            ICartService cartService)
        {
            DbContext = dbContext;
            UserManager = userManager;
            HttpContextAccessor = httpContextAccessor;
            Mapper = mapper;
            CartService = cartService;
        }

        public async Task<OrderListViewModel> GetUserOrdersAsync()
        {
            var userId = UserManager.GetUserId(HttpContextAccessor.HttpContext.User);
            var orders = await DbContext.Orders
                .Where(x => x.User.Id == userId)
                .ToListAsync();

            var result = new OrderListViewModel();
            if (orders?.Any() == true)
            {
                result.Orders = orders.Select(Mapper.Map<OrderViewModel>).ToList();
            }

            return result;
        }

        public async Task CreateOrderFromCartAsync()
        {
            var user = await UserManager.GetUserAsync(HttpContextAccessor.HttpContext.User);
            var cart = await CartService.GetCurrentUserCartAsync();

            DbContext.Orders.Add(new Data.Models.Order
            {
                CreateDate = DateTime.Now,
                DiscountCoef = 1,
                TotalPrice = cart.TotalPrice,
                PriceWithDiscount = cart.TotalPrice,
                User = user,
                ItemToOrders = cart.Items.Select(x => new ItemToOrder
                {
                    AttributesId = x.Attributes.Id,
                    ItemId = x.ItemId,
                    Quantity = x.Quantity,
                    Price = x.Attributes.Price
                }).ToList()
            });

            await DbContext.SaveChangesAsync();
            await CartService.ClearAsync();
        }

        public async Task<OrderViewModel> GetOrderAsync(Guid id)
        {
            var userId = UserManager.GetUserId(HttpContextAccessor.HttpContext.User);
            var order = await DbContext.Orders
                .Include(o => o.ItemToOrders)
                    .ThenInclude(i => i.Item)
                .Include(o => o.ItemToOrders)
                    .ThenInclude(i => i.Attributes)
                .FirstOrDefaultAsync(x => x.Id == id && x.User.Id == userId);

            if (order == null)
            {
                throw new KeyNotFoundException();
            }

            return Mapper.Map<OrderViewModel>(order);
        }
    }
}
