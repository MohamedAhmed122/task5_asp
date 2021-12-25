using SAP.ViewModels.Catalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.ViewModels.Orders
{
    public class OrderItemViewModel
    {
        public Guid ItemId { get; set; }

        public ItemViewModel Item { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public AttributesViewModel Attributes { get; set; }
    }
}
