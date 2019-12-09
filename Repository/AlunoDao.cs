using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class AlunoDao : IRepository<Aluno>
    {
        private readonly Context _context;
        public AlunoDao(Context context)
        {
            _context = context;
        }

        public Aluno BuscarPorId(int id)
        {
            return _context.Aluno.Find(id);
        }

        public bool Cadastrar(Aluno a)
        {
            if (BuscarPorId(a.Id) == null)
            {
                _context.Aluno.Add(a);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Aluno> ListarTodos()
        {
            return _context.Aluno.ToList();
        }

        public void Atualizar(Aluno a)
        {
            _context.Update(a);
            _context.SaveChanges();
        }
        public void Excluir(int id)
        {
            _context.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }
    }
}
