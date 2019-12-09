using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLivraria.Models
{
    public class EmprestimoDto
    {
        public int IdAluno { get; set; }
        public int IdLivro { get; set; }
        public DateTime DataPrevista { get; set; }
    }
}
