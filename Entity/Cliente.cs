using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("Cliente")]
    public class Cliente
    {
        public Cliente()
        {
            CriadoEm = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
