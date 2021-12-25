using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SAP.Services.Order.Implementations
{
    class PromotionCheck : BaseHandler
    {
        IConfiguration _config { get; }

        public PromotionCheck(IConfiguration config)
        {
            _config = config;
        }

        public class PromotionItem
        {
            public int ItemId { get; set; }
            public int Discount { get; set; }
        }

        public class Promotion
        {
            public List<PromotionItem> PromotionItems { get; set; }
        }

        public async override Task<object> Handle(object request)
        {

            var promotion = new Promotion();
            promotion.PromotionItems = new List<PromotionItem>();
            _config.GetSection("PromotionItems").Bind(promotion.PromotionItems);
            return base.Handle(request);
        }
    }
}
