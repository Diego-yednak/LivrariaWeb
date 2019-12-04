using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("Funcionario")]
    public class Funcionario
    {
        public Funcionario()
        {
            CriadoEm = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }
        public string Senha { get; set; }

        [Compare("Senha",
            ErrorMessage = "Os campos não coincidem!")]
        [NotMapped]
        public string ConfirmacaoSenha { get; set; }
        public DateTime CriadoEm { get; set; }
        public string Nivel { get; set; }
    }
}
