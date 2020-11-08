using Desafio.Data;
using Desafio.Models;
using Desafio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Controllers
{
    [Authorize]
    public class FaixasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FaixasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var faixas = await _context.Faixas.Include(faixa => faixa.Album).ToListAsync();

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

        public async Task<IActionResult> Details(Guid id)
        {
            var faixa = await _context.Faixas.Include(faixa => faixa.Album).FirstOrDefaultAsync(faixa => faixa.Id == id);

            if (faixa == null)
            {
                return NotFound();
            }

            return View(new FaixaViewModel
            {
                Id = faixa.Id,
                Nome = faixa.Nome,
                Duracao = faixa.Duracao,
                Ordem = faixa.Ordem,
                AlbumId = faixa.AlbumId,
                Album = faixa.Album.Nome,
                AnoLancamento = faixa.Album.AnoLancamento,
                Artista = faixa.Album.Artista
            });
        }

        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albuns, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FaixaViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Faixa
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    Duracao = model.Duracao,
                    Ordem = model.Ordem,
                    AlbumId = model.AlbumId
                });

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["AlbumId"] = new SelectList(_context.Albuns, "Id", "Nome", model.AlbumId);

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var faixa = await _context.Faixas.FirstOrDefaultAsync(faixa => faixa.Id == id);

            if (faixa == null)
            {
                return NotFound();
            }

            ViewData["AlbumId"] = new SelectList(_context.Albuns, "Id", "Nome", faixa.AlbumId);

            return View(new FaixaViewModel
            {
                Id = faixa.Id,
                Nome = faixa.Nome,
                Duracao = faixa.Duracao,
                Ordem = faixa.Ordem,
                AlbumId = faixa.AlbumId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FaixaViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(new Faixa
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Duracao = model.Duracao,
                    Ordem = model.Ordem,
                    AlbumId = model.AlbumId
                });

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["AlbumId"] = new SelectList(_context.Albuns, "Id", "Nome", model.AlbumId);

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var faixa = await _context.Faixas.Include(faixa => faixa.Album).FirstOrDefaultAsync(faixa => faixa.Id == id);

            if (faixa == null)
            {
                return NotFound();
            }

            return View(new FaixaViewModel
            {
                Id = faixa.Id,
                Nome = faixa.Nome,
                Duracao = faixa.Duracao,
                Ordem = faixa.Ordem,
                AlbumId = faixa.AlbumId,
                Album = faixa.Album.Nome,
                AnoLancamento = faixa.Album.AnoLancamento,
                Artista = faixa.Album.Artista
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var faixa = await _context.Faixas.FirstOrDefaultAsync(faixa => faixa.Id == id);

            _context.Faixas.Remove(faixa);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
