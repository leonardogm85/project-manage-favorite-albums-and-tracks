using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.ViewModels
{
    public class AlbumFavoritoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Ano de lançamento")]
        public int AnoLancamento { get; set; }

        [Display(Name = "Artista")]
        public string Artista { get; set; }

        [Display(Name = "Favorito")]
        public bool Favorito { get; set; }
    }
}
