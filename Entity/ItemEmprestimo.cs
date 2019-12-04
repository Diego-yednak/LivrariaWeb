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
        public Livro Livro { get; set; }
        public int Quantidade { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
