using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/Aluno")]
    [ApiController]
    public class AlunoAPIController : ControllerBase
    {
        //[Route("BuscarPorId/{id}/{teste}")]
        //public IActionResult BuscarPorId(int id, string teste)

        private readonly AlunoDao _alunoDAO;
        public AlunoAPIController(AlunoDao alunoDAO)
        {
            _alunoDAO = alunoDAO;
        }

        //GET: /api/Aluno/ListarTodos
        [HttpGet]
        [Route("ListarTodos")]
        public IActionResult ListarTodos()
        {
            string s = "teste";
            return Ok(_alunoDAO.ListarTodos());
        }

        //GET: /api/Aluno/BuscarPorId
        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public IActionResult BuscarPorId([FromRoute]int id)
        {
            Aluno a = _alunoDAO.BuscarPorId(id);
            if (a != null)
            {
                return Ok(a);
            }
            return NotFound(new { msg = "Aluno não foi encontrado no sistema!" });
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Aluno a)
        {
            if (ModelState.IsValid)
            {
                if (_alunoDAO.Cadastrar(a))
                {
                    return Created("", a);
                }
                return Conflict(new { msg = "Esse aluno já consta no sistema!" });
            }
            return BadRequest(ModelState);
        }
    }
}