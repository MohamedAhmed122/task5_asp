using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Services.Order.Implementations
{
    abstract class BaseHandler : IOrderHandler
    {
        private IOrderHandler _nextHandler;

        public IOrderHandler SetNext(IOrderHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        async public virtual Task<object> Handle(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
