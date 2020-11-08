using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.ViewModels
{
    public class FaixaFavoritaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Duração")]
        public int Duracao { get; set; }

        [Display(Name = "Ordem")]
        public int Ordem { get; set; }

        [Display(Name = "Álbum")]
        public string Album { get; set; }

        [Display(Name = "Favorito")]
        public bool Favorito { get; set; }
    }
}
