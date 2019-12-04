using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebLivraria.Models;
using Repository;
using Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebLivraria.Controllers
{
    public class HomeController : Controller
    {
        private readonly FuncionarioDAO _funcionarioDAO;
        private readonly LivroDAO _livroDAO;
        private readonly UserManager<FuncionarioLogado> _userManager;
        private readonly SignInManager<FuncionarioLogado> _signInManager;

        public HomeController(FuncionarioDAO funcionarioDAO,
            LivroDAO livroDAO,
            UserManager<FuncionarioLogado> userManager,
            SignInManager<FuncionarioLogado> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _livroDAO = livroDAO;
            _funcionarioDAO = funcionarioDAO;
        }

        public IActionResult Index(int? id)
        {
            // return View();
            
            ViewBag.Categorias = _livroDAO.ListarTodos();
            if (id == null)
            {
                return View(_livroDAO.ListarTodos());
            }
            return View(_livroDAO.ListarTodos());
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Funcionario f)
        {
            var result = await _signInManager.PasswordSignInAsync
                (f.Email, f.Senha, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Funcionario");
            }
            ModelState.AddModelError("", "Falha no login!");
            return View(f);
        }

    }
}
