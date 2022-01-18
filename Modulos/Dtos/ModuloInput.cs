using System;
using System.ComponentModel.DataAnnotations;
using RKC.Cursos.Modulos.Abstractions;

namespace RKC.Cursos.Modulos.Dtos
{
    public class ModuloInput : IModulo
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }
    }
}