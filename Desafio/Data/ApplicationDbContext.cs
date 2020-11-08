using Desafio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Desafio.ViewModels;

namespace Desafio.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
    {
        public DbSet<Album> Albuns { get; set; }
        public DbSet<Faixa> Faixas { get; set; }
        public DbSet<FaixaFavorita> FaixasFavoritas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Desafio.ViewModels.FaixaViewModel> FaixaViewModel { get; set; }
    }
}
