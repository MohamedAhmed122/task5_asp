using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SAP.Services.Order.Implementations
{
    public class OrderCheck : IOrderCheck
    {
        private IOrderHandler _handler;

        private void SetHandler(IOrderHandler handler)
        {
            _handler = handler;
        }

        async public Task Check(ICartService cartService, IConfiguration configuration)
        {
            IOrderHandler orderCheck = new StockCheck(cartService);
            IOrderHandler promotionCheck = new PromotionCheck(configuration);
            IOrderHandler personalDiscountsCheck = new PersonalDiscountsCheck(configuration);

            orderCheck.SetNext(promotionCheck).SetNext(personalDiscountsCheck);

            SetHandler(orderCheck);
            await _handler.Handle(new object());
        }
    }
}
