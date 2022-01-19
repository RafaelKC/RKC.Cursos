using System;
using System.ComponentModel.DataAnnotations;
using RKC.Cursos.Aulas.Abstractions;
using RKC.Cursos.Aulas.Dtos;

namespace RKC.Cursos.Aulas
{
    public class Aula : IAula
    {
        public Guid Id { get; set; }
        public Guid ModuloId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        public int Duracao { get; set; }
        public DateTime DataAcontecer { get; set; }
        
        public Aula()
        {}
        
        public Aula(AulaInput aulaInput)
        {
            Id = aulaInput.Id;
            ModuloId = aulaInput.ModuloId;
            Nome = aulaInput.Nome;
            DataAcontecer = aulaInput.DataAcontecer;
            Duracao = aulaInput.Duracao;
        }
        
        public void Update(IAula aulaInput)
        {
            Nome = aulaInput.Nome;
            DataAcontecer = aulaInput.DataAcontecer;
            Duracao = aulaInput.Duracao;
        }
    }
}