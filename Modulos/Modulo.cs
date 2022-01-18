using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RKC.Cursos.Modulos.Abstractions;
using RKC.Cursos.Modulos.Dtos;

namespace RKC.Cursos.Modulos
{
    [Table("Modulos")]
    public class Modulo : IModulo
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }
        
        public Modulo()
        {}

        public void Update(IModulo moduloInput)
        {
            Nome = moduloInput.Nome;
        }
        
        public Modulo(ModuloInput moduloInput)
        {
            Id = moduloInput.Id;
            Nome = moduloInput.Nome;
        }
    }
}