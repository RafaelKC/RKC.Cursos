using System;
using RKC.Cursos.Modulos.Abstractions;

namespace RKC.Cursos.Modulos.Dtos
{
    public class ModuloOutput : IModulo
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int TotalAulas { get; set; }
        public int TotalHorasAula { get; set; }
        

        public ModuloOutput(Modulo moduloInput)
        {
            Id = moduloInput.Id;
            Nome = moduloInput.Nome;
        }
    }
}