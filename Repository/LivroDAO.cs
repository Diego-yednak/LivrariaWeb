using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Repository
{
    public class LivroDAO : IRepository<Livro>
    {
        private readonly Context _context;
         
        public LivroDAO (Context context)
        {
            _context = context;
        }

        public bool Cadastrar(Livro u)
        {
            if (BuscarPorNome(u) == null)
            {
                _context.Livro.Add(u);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Livro BuscarPorNome(Livro u)
        {
            return _context.Livro.FirstOrDefault
                (x => x.Nome.Equals(u.Nome));
        }
        public Livro BuscarPorId(int id)
        {
            return _context.Livro.FirstOrDefault
                (x => x.Id.Equals(id));
        }
        public List<Livro> ListarTodos()
        {
            return _context.Livro.
                ToList();
        }
        public void Excluir(int id)
        {
            _context.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }
        public void Atualizar(Livro l)
        {
            _context.Update(l);
            _context.SaveChanges();
        }
    }
}
