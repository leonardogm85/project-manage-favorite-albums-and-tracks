using Desafio.Data;
using Desafio.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = Enumerable.Empty<ItemFavoritoViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                var usuario = await _context.Users
                    .FirstOrDefaultAsync(usuario => usuario.UserName == User.Identity.Name);

                model = (await _context.FaixasFavoritas
                    .Include(favorito => favorito.Faixa)
                    .ThenInclude(faixa => faixa.Album)
                    .Where(favorito => favorito.UsuarioId == usuario.Id)
                    .Select(favorito => favorito.Faixa)
                    .OrderBy(faixa => faixa.Album.Nome)
                    .ThenBy(faixa => faixa.Ordem)
                    .ToListAsync())
                    .GroupBy(faixa => faixa.Album)
                    .Select(item => new ItemFavoritoViewModel
                    {
                        Id = item.Key.Id,
                        Descricao = $"{item.Key.Nome} [{item.Key.AnoLancamento}] [{item.Key.Artista}]",
                        Subitens = item.Select(subitem => new SubitemFavoritoViewModel
                        {
                            Id = subitem.Id,
                            Descricao = $"{subitem.Ordem}# {subitem.Nome} ({subitem.Duracao})"
                        })
                    });
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
