using System.ComponentModel.DataAnnotations;

namespace UPTEAM.Infra.CrossCutting.Identity.Model
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Nome Completo")]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirma��o n�o combinam.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Phone]
        [StringLength(11)]
        [MaxLength(11)]
        [Display(Name ="Telefone Celular")]
        public string PhoneNumber { get; set; }
    }
}