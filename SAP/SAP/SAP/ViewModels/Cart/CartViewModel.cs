using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.ViewModels.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public List<CartItemViewModel> Items { get; set; }

        public decimal TotalPrice { get; set; } = 0m;
    }
}
