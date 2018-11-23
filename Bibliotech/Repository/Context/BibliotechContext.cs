using Bibliotech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotech.Repository.Context
{
    public class BibliotechContext : DbContext
    {
        public BibliotechContext(DbContextOptions<BibliotechContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Livro> Livro { get; set; }
    }
}
