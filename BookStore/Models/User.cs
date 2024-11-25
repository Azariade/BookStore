using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class User
    {
        [Key]
        public int UsertId { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Votre prénom est obligatoire.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Seulement des lettes sont accepter pour votre prénom.")]
        [StringLength(100, ErrorMessage = "Votre prénom ne doit pas dépassé 100 charactères.")]
        public required string FirstName { get; set; }

        [Display(Name = "Nom de famille")]
        [Required(ErrorMessage = "Votre nom de famille est obligatoire.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Seulement des lettes sont accepter pour votre nom de famille.")]
        [StringLength(100, ErrorMessage = "Votre nom de famille ne doit pas dépassé 100 charactères")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Votre email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Vote email doit respecter ce format: exemple@domaine.com.")]
        public required string Email { get; set; }

        [Display(Name = "Numéro de téléphone")]
        [Required(ErrorMessage = "Votre numéro de téléphone est obligatoire.")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Le numéro de téléphone doit être au format international, par exemple : +1234567890.")]
        public required string PhoneNumber { get; set; }

        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Le mot de passe doit contenir au moins une lettre et un chiffre.")]
        public required string Password { get; set; }

    }
}
