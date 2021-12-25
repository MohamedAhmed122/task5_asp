using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SAP.Services.Order.Implementations
{
    class PersonalDiscountsCheck : BaseHandler
    {
        IConfiguration _config { get; }

        public PersonalDiscountsCheck(IConfiguration config)
        {
            _config = config;
        }

        public class Loyalty
        {
            public int UserOrdersAmount { get; set; }
            public int Discount { get; set; }
        }

        public async override Task<object> Handle(object request)
        {
            var loyalty = new Loyalty();
            _config.GetSection("Loyalty").Bind(loyalty);
            return base.Handle(request);
        }
    }
}
