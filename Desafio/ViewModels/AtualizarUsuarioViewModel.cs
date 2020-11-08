using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.ViewModels
{
    public class AtualizarUsuarioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9\-\._@\+]*$", ErrorMessage = "O campo {0} deve ser valido.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [DataType(DataType.Date, ErrorMessage = "O campo {0} deve ser valido.")]
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [StringLength(1, ErrorMessage = "O campo {0} deve ter 1 caracter.", MinimumLength = 1)]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "O campo {0} deve ser valido.")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "O campo {0} deve ser valido.")]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "O campo Senha e Confirme a senha devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }
}
