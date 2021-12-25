using System;
using System.ComponentModel.DataAnnotations;

namespace SAP.Data.Models.Catalogue
{
    public class Attributes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Colors Color { get; set; }

        [Required]
        public double Width { get; set; }

        [Required]
        public double Height { get; set; }

        [Required]
        public double Length { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        public virtual Item Item { get; set; }

        [Required]
        public decimal Price { get; set; }
    }

    public enum Colors
    {
        Black,
        Green,
        Red,
        White,
        Silver,
        Gold,
        Orange
    }
}
