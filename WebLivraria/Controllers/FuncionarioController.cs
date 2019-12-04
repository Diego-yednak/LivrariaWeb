using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entity;
using Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebLivraria.Controllers
{
    // [Authorize]
    public class FuncionarioController : Controller
    {
        // private readonly Context _context;
        private readonly FuncionarioDAO _funcionarioDAO;
        private readonly UserManager<FuncionarioLogado> _userManager;
        private readonly SignInManager<FuncionarioLogado> _signInManager;

        public FuncionarioController(FuncionarioDAO funcionarioDAO,
            UserManager<FuncionarioLogado> userManager,
            SignInManager<FuncionarioLogado> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _funcionarioDAO = funcionarioDAO;
        }

        // GET: Funcionarios
        public IActionResult Index()
        {
            return View(_funcionarioDAO.ListarTodos() );
        }

        // GET: Funcionarios/Details/5
        public IActionResult Details(int id)
        {
            Funcionario funcionario = _funcionarioDAO
                .BuscarPorId(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Funcionario f)
        {
            if (ModelState.IsValid)
            {
                // funcionario.CriadoEm = DateTime.Now;
                // _funcionarioDAO.Cadastrar(funcionario);


                FuncionarioLogado uLogado = new FuncionarioLogado
                {
                    UserName = f.Email,
                    Email = f.Email
                };

                IdentityResult result =
                    await _userManager.CreateAsync(uLogado, f.Senha);

                if (result.Succeeded)
                {
                    //Token de confirmação do email
                    string token = await
                        _userManager.GenerateEmailConfirmationTokenAsync(uLogado);
                    //Autenticação do usuário
                    await _signInManager.
                        SignInAsync(uLogado, isPersistent: false);

                    if (_funcionarioDAO.Cadastrar(f))
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Esse e-mail já está sendo usado!");
                }
                AdicionarErros(result);
                // return RedirectToAction(nameof(Create));
            }
            return View(f);
        }

        // GET: Funcionarios/Edit/5
        public IActionResult Edit(int id)
        {
            Funcionario funcionario = _funcionarioDAO.BuscarPorId(id);
            funcionario.ConfirmacaoSenha = funcionario.Senha;
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Funcionario f)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _funcionarioDAO.Atualizar(f);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(f);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Funcionario f = _funcionarioDAO.BuscarPorId(id);
            FuncionarioLogado funcionarioLogado =
                await _userManager.FindByEmailAsync(f.Email);

            IdentityResult result =
                    await _userManager.DeleteAsync(funcionarioLogado);

            if (result.Succeeded)
            {
                _funcionarioDAO.Excluir(id);
            }
            return RedirectToAction("Index");
        }
        private void AdicionarErros(IdentityResult result)
        {
            foreach (var erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
