using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RKC.Cursos.Aulas.Services;
using RKC.Cursos.Modulos.Dtos;
using RKC.Cursos.Modulos.Enums;
using RKC.Cursos.Modulos.Services;

namespace RKC.Cursos.Modulos.Controllers
{
    [Route("cursos/modulos")]
    public class ModuloController : ControllerBase
    {
        private readonly IModuloRepositoryService _moduloRepository;
        private readonly IAulaRepositoryService _aulaRepository;

        public ModuloController(IModuloRepositoryService moduloRepository, IAulaRepositoryService aulaRepository)
        {
            _moduloRepository = moduloRepository;
            _aulaRepository = aulaRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] ModuloInput moduloInput)
        {
            var moduloResult = await _moduloRepository.Create(moduloInput);

            return moduloResult switch
            {
                ModuloRepositoryResult.ModuloAlredyCreated => new UnprocessableEntityObjectResult(moduloInput),
                _ => Ok()
            };
        }
        
        [HttpGet("{idModulo:guid}")]
        [Authorize]
        public async Task<ActionResult<ModuloOutput>> Get([FromRoute] Guid idModulo)
        {
            var modulo = await _moduloRepository.Get(idModulo);
            if (modulo == null) return NotFound();
            
            var aulas = await _aulaRepository.GetList(new List<Guid>{modulo.Id}, null);
            modulo.TotalAulas = aulas.Count;
            modulo.TotalHorasAula = aulas.Sum(aula => aula.CargaHoraria);
            
            return Ok(modulo);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ModuloOutput>>> GetList([FromQuery] string nomeFilter)
        {
            var modulos = await _moduloRepository.GetList(nomeFilter);
            var modulosIds = modulos.Select(modulo => modulo.Id).ToList();

            var aulas = await _aulaRepository.GetList(modulosIds, null);
            
            foreach (var modulo in modulos)
            {
                var aulasPorModulo = aulas.Where(aula => aula.ModuloId == modulo.Id).ToList();
                if(!aulasPorModulo.Any()) continue;

                modulo.TotalAulas = aulasPorModulo.Count;
                modulo.TotalHorasAula = aulasPorModulo.Sum(aula => aula.CargaHoraria);
            }
            
            return Ok();
        }
        
        [HttpPut("{idModulo:guid}")]
        [Authorize]
        public async Task<ActionResult> Update([FromRoute] Guid idModulo, [FromBody] ModuloInput moduloInput)
        {
            var moduloResult = await _moduloRepository.Update(idModulo, moduloInput);

            return moduloResult switch
            {
                ModuloRepositoryResult.NotFound => NotFound(),
                _ => Ok()
            };
        }
        
        [HttpDelete("{idModulo:guid}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] Guid idModulo)
        {
            var moduloResult = await _moduloRepository.Delete(idModulo);

            return moduloResult switch
            {
                ModuloRepositoryResult.NotFound => NotFound(),
                _ => Ok()
            };
        }
    }
}