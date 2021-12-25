using Microsoft.AspNetCore.Identity;
using SAP.Data.Models.Catalogue;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Data.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        public ICollection<ItemToCart> ItemToCarts { get; set; }
    }
}
