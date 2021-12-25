using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAP.Services.Implementations;


namespace SAP.Services.Order.Implementations
{
    class StockCheck : BaseHandler
    {
        ICartService CartService { get; }

        public StockCheck(ICartService cartService)
        {
            CartService = cartService;
        }

        public async override Task<object> Handle(object request)
        {
            var cart = await CartService.GetCurrentUserCartAsync();
            foreach (var item in cart.Items)
            {
                if (item.Quantity > item.Item.QuantityInStock)
                    throw new Exception();
                else
                {
                    return base.Handle(request);
                }
            }
            return base.Handle(request);
        }
    }
}
