using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SAP.ViewModels.Catalogue.Admin
{
    [DataContract]
    public class CreateItemViewModel
    {
        [DataMember]
        [Required]
        public Guid CategoryId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [MaxLength(50)]
        [Required]
        public string ManufacturerId { get; set; }

        [DataMember]
        public int QuantityInStock { get; set; }

        [DataMember]
        [Required]
        [MaxLength(2000)]
        public string PictureUrl { get; set; }

        [DataMember]
        public List<AttributesViewModel> Attributes { get; set; }
    }
}
