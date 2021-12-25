using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Data.Models.Catalogue
{
    [Table("Items")]
    public class Item : Node
    {
        //[Required]
        public Guid? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        [MaxLength(50)]
        public string ManufacturerId { get; set; }

        public int QuantityInStock { get; set; }

        [Required]
        [MaxLength(2000)]
        public string PictureUrl { get; set; }

        public virtual ICollection<Attributes> Attributes { get; set; }

        public virtual ICollection<ItemToCart> ItemToCarts { get; set; }

        public virtual ICollection<ItemToOrder> ItemToOrders { get; set; }
    }
}
