using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.ViewModels
{
    public class VisualizarUsuarioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Data de nascimento")]
        public string DataNascimento { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }
    }
}
