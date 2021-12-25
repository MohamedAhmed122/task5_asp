using SAP.Data.Models.Catalogue;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Data.Models
{
    public class ItemToCart
    {
        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public int Quantity { get; set; }

        public int AttributesId { get; set; }

        public virtual Attributes Attributes { get; set; }
    }
}
