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
            throw new NotImplementedException();
        }

        public bool Cadastrar(Aluno objeto)
        {
            throw new NotImplementedException();
        }

        public List<Aluno> ListarTodos()
        {
            return _context.Aluno.ToList();
        }
    }
}
