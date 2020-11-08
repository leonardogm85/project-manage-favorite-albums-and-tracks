using Desafio.Models;
using Desafio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public UsuariosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _userManager.Users.ToListAsync();

            return View(usuarios.Select(usuario => new VisualizarUsuarioViewModel
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                Nome = usuario.Nome,
                DataNascimento = usuario.DataNascimento.ToShortDateString(),
                Sexo = usuario.Sexo
            }));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(usuario => usuario.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(new VisualizarUsuarioViewModel
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                Nome = usuario.Nome,
                DataNascimento = usuario.DataNascimento.ToShortDateString(),
                Sexo = usuario.Sexo
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdicionarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.Users.AnyAsync(usuario => usuario.UserName == model.UserName))
                {
                    ModelState.AddModelError(nameof(AdicionarUsuarioViewModel.UserName), "O Username já está sendo usado.");
                }
                else
                {
                    var user = new Usuario
                    {
                        UserName = model.UserName,
                        Nome = model.Nome,
                        DataNascimento = model.DataNascimento,
                        Sexo = model.Sexo
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    result.Errors.ToList().ForEach(error => ModelState.AddModelError(string.Empty, error.Description));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(usuario => usuario.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(new AtualizarUsuarioViewModel
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                Nome = usuario.Nome,
                DataNascimento = usuario.DataNascimento,
                Sexo = usuario.Sexo
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AtualizarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.Users.AnyAsync(usuario => usuario.Id != model.Id && usuario.UserName == model.UserName))
                {
                    ModelState.AddModelError(nameof(AdicionarUsuarioViewModel.UserName), "O Username já está sendo usado.");
                }
                else
                {
                    var usuario = await _userManager.Users.FirstOrDefaultAsync(usuario => usuario.Id == model.Id);

                    usuario.UserName = model.UserName;
                    usuario.Nome = model.Nome;
                    usuario.DataNascimento = model.DataNascimento;
                    usuario.Sexo = model.Sexo;

                    var result = await _userManager.UpdateAsync(usuario);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            await _userManager.RemovePasswordAsync(usuario);

                            await _userManager.AddPasswordAsync(usuario, model.Password);
                        }

                        return RedirectToAction(nameof(Index));
                    }

                    result.Errors.ToList().ForEach(error => ModelState.AddModelError(string.Empty, error.Description));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(usuario => usuario.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(new VisualizarUsuarioViewModel
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                Nome = usuario.Nome,
                DataNascimento = usuario.DataNascimento.ToShortDateString(),
                Sexo = usuario.Sexo
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(usuario => usuario.Id == id);

            await _userManager.DeleteAsync(usuario);

            return RedirectToAction(nameof(Index));
        }
    }
}
