using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class ItemEmprestimo
    {
        public ItemEmprestimo()
        {
            CriadoEm = DateTime.Now;
            Livro = new Livro();
        }

        [Key]
        public int Id { get; set; }
        public Funcionario Funcionario { get; set; }
        public Aluno Aluno { get; set; }
        public Livro Livro { get; set; }
        public int Quantidade { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime DataDevolucao { get; set; }
        public DateTime DataPrevista { get; set; }
    }
}
