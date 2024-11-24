using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "La commande associée est obligatoire.")]
        public int OrderId { get; set; }
        [Required]
        [ForeignKey("OrderId")]
        public Order Order { get; set; } // Relation avec l'entité Order

        [Required(ErrorMessage = "Le produit associé est obligatoire.")]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; } // Relation avec l'entité Product

        [Required(ErrorMessage = "La quantité est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être au minimum de 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Le prix unitaire est obligatoire.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix unitaire doit être une valeur positive.")]
        public decimal UnitPrice => Product.Price;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant total doit être une valeur positive.")]
        public decimal TotalAmount => UnitPrice * Quantity;
    }
}
