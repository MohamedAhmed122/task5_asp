using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAP.Services.Order.Implementations;
using Microsoft.Extensions.Configuration;

namespace SAP.Services.Order
{
    public interface IOrderCheck
    {
        Task Check(ICartService cartService, IConfiguration configuration);
    }
}
