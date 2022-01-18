using System;

namespace RKC.Cursos.Aulas.Abstractions
{
    public interface IAula
    {
        public Guid IdModulo { get; set; } 
        public string Nome { get; set; }
        public DateTime DataAcontecer { get; set; }
    }
}