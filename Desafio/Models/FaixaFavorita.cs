using System;

namespace Desafio.Models
{
    public class FaixaFavorita
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid FaixaId { get; set; }
        public Usuario Usuario { get; set; }
        public Faixa Faixa { get; set; }
    }
}
