using System;
using System.Collections.Generic;

namespace Desafio.Models
{
    public class Faixa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }
        public int Ordem { get; set; }
        public Guid AlbumId { get; set; }
        public Album Album { get; set; }
        public IEnumerable<FaixaFavorita> FaixasFavoritas { get; set; }
    }
}
