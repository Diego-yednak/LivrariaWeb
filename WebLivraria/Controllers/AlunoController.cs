using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entity;
using Repository;
using WebLivraria.Models;

namespace WebLivraria.Controllers
{
    public class AlunoController : Controller
    {
        private readonly AlunoDao _alunoDao;
        private readonly LivroDAO _livroDAO;
        private readonly ItemEmprestimoDao _itemEmprestimoDao;
        private readonly FuncionarioDAO _funcionarioDAO;

        public AlunoController(AlunoDao alunoDao, 
            LivroDAO livroDAO,
            ItemEmprestimoDao itemEmprestimoDao,
            FuncionarioDAO funcionarioDao)
        {
            _livroDAO = livroDAO;
            _alunoDao = alunoDao;
            _itemEmprestimoDao = itemEmprestimoDao;
            _funcionarioDAO = funcionarioDao;
        }

        // GET: Aluno
        public IActionResult Index()
        {
            return View(_alunoDao.ListarTodos());
        }

        // GET: Aluno/Details/5
        public IActionResult Details(int id)
        {

            ViewBag.itemEmprestimo = _itemEmprestimoDao.ListarPorIdAluno(id);
            ViewBag.todosLivros =
                                new SelectList(_livroDAO.ListarTodos(),
                "Id", "Nome");

            Aluno aluno = _alunoDao.BuscarPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Aluno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aluno a)
        {
            if (ModelState.IsValid)
            {
                if(_alunoDao.Cadastrar(a))
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(a);
        }

        // GET: Aluno/Edit/5
        public IActionResult Edit(int id)
        {

            Aluno aluno = _alunoDao.BuscarPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Aluno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Matricula,CriadoEm")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _alunoDao.Atualizar(aluno);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Aluno/Delete/5
        public IActionResult Delete(int id)
        {
            Aluno aluno = _alunoDao.BuscarPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _alunoDao.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            Aluno a = _alunoDao.BuscarPorId(id);
            if (a != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        // public IActionResult AdicionarLivro(int IdUsuario, int idLivro)
        public IActionResult AdicionarLivro(EmprestimoDto emprestimoDto)
        {
            string urlAnterior = Request.Headers["Referer"].ToString();
            // int id = 8; // FUNCIONARIO TESTE
            int idFuncionario = 1;
            Livro livroEmprestimo = _livroDAO.BuscarPorId(emprestimoDto.IdLivro);
            if(livroEmprestimo != null)
            {
                ItemEmprestimo itemEmprestimo = new ItemEmprestimo();
                itemEmprestimo.Livro = livroEmprestimo;
                itemEmprestimo.Aluno = _alunoDao.BuscarPorId(emprestimoDto.IdAluno);
                itemEmprestimo.Funcionario = _funcionarioDAO.BuscarPorId(idFuncionario);
                
                _itemEmprestimoDao.Cadastrar(itemEmprestimo);
            }
            // livrosParaEmprestar.Add(livroEmprestimo);
            // return RedirectToAction(nameof(Details), "Aluno" , new { emprestimoDto.IdAluno } );
            return Redirect(urlAnterior);
        }

        public IActionResult Devolucao(int id)
        {
            string urlAnterior = Request.Headers["Referer"].ToString();

            ItemEmprestimo itemEmprestimo = _itemEmprestimoDao.BuscarPorId(id);
            if(itemEmprestimo != null)
            {
                itemEmprestimo.DataDevolucao = DateTime.Now;
                _itemEmprestimoDao.Atualizar(itemEmprestimo);
            }
            
            // var teste = Response.Redirect(Request.UrlReferrer.ToString());
            return Redirect(urlAnterior);
        }
    }
}
