using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Entity;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json;
using WebLivraria.Models;

namespace WebLivraria.Controllers
{
    public class LivroController : Controller
    {
        private readonly LivroDAO _livroDAO;

        public LivroController (LivroDAO livroDAO)
        {
            _livroDAO = livroDAO;
        }
        public IActionResult Index()
        {
            return View(_livroDAO.ListarTodos());
        }

        [HttpPost]
        public IActionResult Cadastrar(Livro l)
        {
            if(_livroDAO.Cadastrar(l))
            {
                return RedirectToAction("Index");
            }
                
            ModelState.AddModelError
                ("", "Esse produto já existe!");
            return View(l);
        }
        

        public IActionResult Cadastrar()
        {
            
            Livro l = new Livro();
            
            if (TempData["Livro"] != null)
            {
                LivroDto dto = new LivroDto();
                string resultado = TempData["Livro"].ToString();
                // Newtonsoft.Json
                dto = JsonConvert.
                    DeserializeObject<LivroDto>(resultado);

                try
                {
                    l.Nome = dto.items[0].volumeInfo.title;
                    l.Autor = dto.items[0].volumeInfo.authors[0];
                    l.Prefacio = dto.items[0].volumeInfo.description;
                    l.Categoria = dto.items[0].volumeInfo.categories[0];
                    l.DataPublicacao = Convert.ToDateTime(dto.items[0].volumeInfo.publishedDate);
                    l.LinkImagem = dto.items[0].volumeInfo.imageLinks.smallThumbnail;
                } catch(Exception e)
                {
                    View(l);
                }

            }
            
            return View(l);
        }

        [HttpPost]
        public IActionResult BuscarInfoLivro(Livro l)
        {
            string url = $"https://www.googleapis.com/books/v1/volumes?q={l.Nome}&maxResults=1";
            WebClient client = new WebClient();
            TempData["Livro"] = client.DownloadString(url);
            return RedirectToAction(nameof(Cadastrar));
        }

        public IActionResult Detalhes(int id)
        {
            Livro livro = _livroDAO
                .BuscarPorId(id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        public IActionResult Remover(int id)
        {
            _livroDAO.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Alterar(int id)
        {
            Livro livro = _livroDAO.BuscarPorId(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost]
        public IActionResult Alterar(Livro l)
        {
            _livroDAO.Atualizar(l);
            return RedirectToAction("Index");
        }
    }
}