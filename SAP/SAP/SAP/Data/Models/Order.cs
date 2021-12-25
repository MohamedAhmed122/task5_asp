using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Data.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public ICollection<ItemToOrder> ItemToOrders { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DiscountCoef { get; set; }

        public decimal PriceWithDiscount { get; set; }
    }
}
