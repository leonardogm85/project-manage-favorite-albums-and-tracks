using System;
using System.Collections.Generic;

namespace Desafio.ViewModels
{
    public class ItemFavoritoViewModel
    {
        public Guid Id { get; set; }

        public string Descricao { get; set; }

        public IEnumerable<SubitemFavoritoViewModel> Subitens { get; set; }
    }
}
