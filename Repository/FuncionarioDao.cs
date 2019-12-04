using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class FuncionarioDAO : IRepository<Funcionario>
    {
        private readonly Context _context;
        public FuncionarioDAO(Context context)
        {
            _context = context;
        }
        public Funcionario BuscarPorId(int id)
        {
            return _context.Funcionario.Find(id);
        }
        public List<Funcionario> ListarTodos()
        {
            return _context.Funcionario.ToList();
        }
        public bool Cadastrar(Funcionario f)
        {
            if (BuscarPorEmail(f) == null)
            {
                _context.Funcionario.Add(f);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public Funcionario BuscarPorEmail(Funcionario f)
        {
            return _context.Funcionario.FirstOrDefault
                (x => x.Email.Equals(f.Email));
        }

        public void Atualizar (Funcionario f)
        {
            _context.Update(f);
            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            _context.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }
    }
}
