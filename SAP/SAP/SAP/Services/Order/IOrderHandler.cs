using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Services.Order
{
    public interface IOrderHandler
    {
        IOrderHandler SetNext(IOrderHandler handler);

        Task<object> Handle(object request);
    }
}
