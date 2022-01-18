using System;
using System.ComponentModel.DataAnnotations;
using RKC.Cursos.Aulas.Abstractions;

namespace RKC.Cursos.Aulas.Dtos
{
    public class AulaInput : IAula
    {
        public Guid Id { get; set; }
        public Guid ModuloId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        public int CargaHoraria { get; set; }
        public DateTime DataAcontecer { get; set; }
    };
}