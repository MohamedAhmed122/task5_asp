using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SAP.ViewModels.Catalogue
{
    public class ItemViewModel : NodeViewModel
    {
        public Guid CategoryId { get; set; }

        public CategoryViewModel Category { get; set; }

        public string ManufacturerId { get; set; }

        public int QuantityInStock { get; set; }

        public bool IsInStock => QuantityInStock > 0;

        public string PictureUrl { get; set; }

        public List<AttributesViewModel> Attributes { get; set; }

        public SelectList AttributesToSelect { get; set; }

        public bool IsInCart { get; set; }
    }
}
