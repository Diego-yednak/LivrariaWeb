using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class Context : IdentityDbContext<FuncionarioLogado>
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }
        public DbSet<Aluno> Cliente { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<ItemEmprestimo> ItemEmprestimo { get; set; }
        public DbSet<Livro> Livro { get; set; }
    }
}
