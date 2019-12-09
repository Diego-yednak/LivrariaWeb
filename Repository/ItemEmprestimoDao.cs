using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ItemEmprestimoDao : IRepository<ItemEmprestimo>
    {
        private readonly Context _context;

        public ItemEmprestimoDao(Context context)
        {
            _context = context;
        }
        public ItemEmprestimo BuscarPorId(int id)
        {
            return _context.ItemEmprestimo.Find(id);
        }

        public bool Cadastrar(ItemEmprestimo i)
        {
            if (BuscarPorId(i.Id) == null)
            {
                _context.ItemEmprestimo.Add(i);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ItemEmprestimo> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public List<ItemEmprestimo> ListarPorIdAluno(int id)
        {
            return _context.
                ItemEmprestimo.
                Include(x => x.Livro).
                Include(x => x.Funcionario).
                Where(x => x.Aluno.Id == id &&
                       x.DataDevolucao == new DateTime()
                ).ToList();
        }
        public void Atualizar(ItemEmprestimo i)
        {
            _context.Update(i);
            _context.SaveChanges();
        }
    }
}
