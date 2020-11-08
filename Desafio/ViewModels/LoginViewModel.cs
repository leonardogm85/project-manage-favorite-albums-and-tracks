using System.ComponentModel.DataAnnotations;

namespace Desafio.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [DataType(DataType.Password, ErrorMessage = "O campo {0} deve ser valido.")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar de mim")]
        public bool RememberMe { get; set; }
    }
}
