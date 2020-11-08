using Desafio.Data;
using Desafio.Models;
using Desafio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Controllers
{
    [Authorize]
    public class FavoritosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoritosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(usuario => usuario.UserName == User.Identity.Name);

            var favoritos = await _context.FaixasFavoritas
                .Include(favorito => favorito.Faixa)
                .Where(favorito => favorito.UsuarioId == usuario.Id)
                .Select(favorito => favorito.Faixa.AlbumId)
                .Distinct()
                .ToListAsync();

            var albuns = await _context.Albuns
                .Where(album => album.Faixas.Any())
                .OrderBy(album => album.Nome)
                .Select(album => new AlbumFavoritoViewModel
                {
                    Id = album.Id,
                    Nome = album.Nome,
                    AnoLancamento = album.AnoLancamento,
                    Artista = album.Artista,
                    Favorito = favoritos.Any(favoritoId => favoritoId == album.Id)
                })
                .ToListAsync();

            return View(albuns);
        }

        public async Task<IActionResult> Faixas(Guid id)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(usuario => usuario.UserName == User.Identity.Name);

            var favoritos = await _context.FaixasFavoritas
                .Where(favorito => favorito.UsuarioId == usuario.Id)
                .Select(favorito => favorito.FaixaId)
                .Distinct()
                .ToListAsync();

            var album = await _context.Albuns
                .FirstOrDefaultAsync(album => album.Id == id);

            ViewData["Album"] = album.Nome;

            var faixas = await _context.Faixas
                .Include(faixa => faixa.Album)
                .Where(faixa => faixa.AlbumId == id)
                .OrderBy(faixa => faixa.Ordem)
                .Select(faixa => new FaixaFavoritaViewModel
                {
                    Id = faixa.Id,
                    Nome = faixa.Nome,
                    Duracao = faixa.Duracao,
                    Ordem = faixa.Ordem,
                    Album = faixa.Album.Nome,
                    Favorito = favoritos.Any(favoritoId => favoritoId == faixa.Id)
                })
                .ToListAsync();

            return View(faixas);
        }

        public async Task<IActionResult> AdicionarAlbum(Guid id)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(usuario => usuario.UserName == User.Identity.Name);

            var faixas = await _context.Faixas
                .Where(faixa => faixa.AlbumId == id)
                .ToListAsync();

            faixas
                .ForEach(faixa => _context.Add(new FaixaFavorita
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuario.Id,
                    FaixaId = faixa.Id
                }));

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AdicionarFaixa(Guid id)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(usuario => usuario.UserName == User.Identity.Name);

            var faixa = await _context.Faixas
                .FirstOrDefaultAsync(faixa => faixa.Id == id);

            _context.Add(new FaixaFavorita
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuario.Id,
                FaixaId = faixa.Id
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Faixas), new { id = faixa.AlbumId });
        }

        public async Task<IActionResult> RemoverAlbum(Guid id)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(usuario => usuario.UserName == User.Identity.Name);

            var faixasFavoritas = await _context.FaixasFavoritas
                .Include(faixaFavorita => faixaFavorita.Faixa)
                .Where(faixaFavorita => faixaFavorita.Faixa.AlbumId == id)
                .ToListAsync();

            _context.RemoveRange(faixasFavoritas);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoverFaixa(Guid id)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(usuario => usuario.UserName == User.Identity.Name);

            var faixaFavorita = await _context.FaixasFavoritas
                .Include(faixaFavorita => faixaFavorita.Faixa)
                .FirstOrDefaultAsync(faixaFavorita => faixaFavorita.FaixaId == id);

            _context.RemoveRange(faixaFavorita);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Faixas), new { id = faixaFavorita.Faixa.AlbumId });
        }
    }
}
