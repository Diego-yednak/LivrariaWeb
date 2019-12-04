using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("Livro")]
    public class Livro
    {
        public Livro()
        {
            CriadoEm = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Prefacio { get; set; }
        public string LinkImagem { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
