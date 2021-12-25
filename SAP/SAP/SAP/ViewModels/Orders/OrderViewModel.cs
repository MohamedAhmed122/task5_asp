using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.ViewModels.Orders
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<OrderItemViewModel> OrderItems { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DiscountCoef { get; set; }

        public decimal PriceWithDiscount { get; set; }
    }
}
