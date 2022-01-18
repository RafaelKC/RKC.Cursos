using System;
using RKC.Cursos.Aulas.Abstractions;

namespace RKC.Cursos.Aulas
{
    public class Aula : IAula
    {
        public Guid Id { get; set; }
        public Guid IdModulo { get; set; }
        public string Nome { get; set; }
        public DateTime DataAcontecer { get; set; }
    }
}