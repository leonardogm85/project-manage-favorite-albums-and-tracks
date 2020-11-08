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
    public class AlbunsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbunsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var albuns = await _context.Albuns.ToListAsync();

            return View(albuns.Select(album => new AlbumViewModel
            {
                Id = album.Id,
                Nome = album.Nome,
                AnoLancamento = album.AnoLancamento,
                Artista = album.Artista
            }));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var album = await _context.Albuns.FirstOrDefaultAsync(album => album.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(new AlbumViewModel
            {
                Id = album.Id,
                Nome = album.Nome,
                AnoLancamento = album.AnoLancamento,
                Artista = album.Artista
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Album
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    AnoLancamento = model.AnoLancamento,
                    Artista = model.Artista
                });

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var album = await _context.Albuns.FirstOrDefaultAsync(album => album.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(new AlbumViewModel
            {
                Id = album.Id,
                Nome = album.Nome,
                AnoLancamento = album.AnoLancamento,
                Artista = album.Artista
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AlbumViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(new Album
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    AnoLancamento = model.AnoLancamento,
                    Artista = model.Artista
                });

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var album = await _context.Albuns.FirstOrDefaultAsync(album => album.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(new AlbumViewModel
            {
                Id = album.Id,
                Nome = album.Nome,
                AnoLancamento = album.AnoLancamento,
                Artista = album.Artista
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var album = await _context.Albuns.FirstOrDefaultAsync(album => album.Id == id);

            _context.Albuns.Remove(album);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> List(Guid id)
        {
            var faixas = await _context.Faixas.Include(faixa => faixa.Album).Where(faixa => faixa.AlbumId == id).ToListAsync();

            var album = await _context.Albuns.FirstOrDefaultAsync(album => album.Id == id);

            ViewData["Album"] = album.Nome;

            return View(faixas.Select(faixa => new FaixaViewModel
            {
                Id = faixa.Id,
                Nome = faixa.Nome,
                Duracao = faixa.Duracao,
                Ordem = faixa.Ordem,
                AlbumId = faixa.AlbumId,
                Album = faixa.Album.Nome,
                AnoLancamento = faixa.Album.AnoLancamento,
                Artista = faixa.Album.Artista
            }));
        }
    }
}
