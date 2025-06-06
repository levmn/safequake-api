using System.ComponentModel.DataAnnotations;

namespace SafeQuake.MVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Endereço é obrigatório")]
        [Display(Name = "Endereço")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Data de Criação")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
} 