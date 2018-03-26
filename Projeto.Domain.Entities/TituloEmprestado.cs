using System;

namespace Projeto.Domain.Entities
{
    public class TituloEmprestado
    {
        public Emprestimo Emprestimo { get; set; }
        public Titulo Titulo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}
