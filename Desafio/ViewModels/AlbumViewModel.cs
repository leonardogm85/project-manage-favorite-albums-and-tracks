﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.ViewModels
{
    public class AlbumViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [Range(1, 9999, ErrorMessage = "O campo {0} deve estar entre 1 e 9999.")]
        [Display(Name = "Ano de lançamento")]
        public int AnoLancamento { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
        [Display(Name = "Artista")]
        public string Artista { get; set; }
    }
}
