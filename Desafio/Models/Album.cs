using System;
using System.Collections.Generic;

namespace Desafio.Models
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int AnoLancamento { get; set; }
        public string Artista { get; set; }
        public IEnumerable<Faixa> Faixas { get; set; }
    }
}
