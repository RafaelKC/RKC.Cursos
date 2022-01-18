using System;
using RKC.Cursos.Aulas.Abstractions;

namespace RKC.Cursos.Aulas.Dtos
{
    public class AulaOutput : IAula
    {
        public Guid Id { get; set; }
        public Guid ModuloId { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public DateTime DataAcontecer { get; set; }

        public AulaOutput(Aula aulaInput)
        {
            Id = aulaInput.Id;
            ModuloId = aulaInput.ModuloId;
            Nome = aulaInput.Nome;
            DataAcontecer = aulaInput.DataAcontecer;
            CargaHoraria = aulaInput.CargaHoraria;
            
        }
    }
}