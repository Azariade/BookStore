using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Le nom du produit est obligatoire.")]
        [StringLength(150)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Le prix est obligatoire.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être un nombre positif.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La quantité en stock est obligatoire.")]
        [Range(0, int.MaxValue, ErrorMessage = "La quantité en stock doit être un entier positif ou nul.")]
        public int StockQuantity { get; set; }
    }
}
