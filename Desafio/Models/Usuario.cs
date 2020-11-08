using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Desafio.Models
{
    public class Usuario : IdentityUser<Guid>
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public IEnumerable<FaixaFavorita> FaixasFavoritas { get; set; }
    }
}
