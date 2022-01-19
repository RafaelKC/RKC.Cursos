using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RKC.Cursos.Aulas.Dtos;
using RKC.Cursos.Aulas.Enums;
using RKC.Cursos.Aulas.Services;

namespace RKC.Cursos.Aulas.Controllers
{
    [Route("cursos/modulos/{moduloId:guid}/aulas")]
    public class AulaController : ControllerBase
    {
        private readonly IAulaRepositoryService _aulaRepository;

        public AulaController(IAulaRepositoryService aulaRepository)
        {
            _aulaRepository = aulaRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromRoute] Guid moduloId, [FromBody] AulaInput aulaInput)
        {
            var aulaResult = await _aulaRepository.Create(moduloId, aulaInput);

            return aulaResult switch
            {
                AulaRepositoryResult.AulaAlredyCreated => new UnprocessableEntityObjectResult(aulaInput),
                _ => Ok()
            };
        }
        
        [HttpGet("{aulaId:guid}")]
        [Authorize]
        public async Task<ActionResult<AulaOutput>> Get([FromRoute] Guid moduloId, [FromBody] Guid aulaId)
        {
            var aula = await _aulaRepository.Get(moduloId, aulaId);
            if (aula == null) return NotFound();

            return Ok(aula);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AulaOutput>>> GetList([FromRoute] Guid moduloId, [FromQuery] string filterNome)
        {
            return Ok(await _aulaRepository.GetList(new List<Guid>{moduloId}, filterNome));
        }
        
        [HttpPut("{aulaId:guid}")]
        [Authorize]
        public async Task<ActionResult> Update([FromRoute] Guid moduloId, [FromRoute] Guid aulaId, [FromBody] AulaInput aulaInput)
        {
            var aulaResult = await _aulaRepository.Update(moduloId, aulaId, aulaInput);

            return aulaResult switch
            {
                AulaRepositoryResult.NotFound => new NotFoundResult(),
                _ => Ok()
            };
        }
        
        [HttpDelete("{aulaId:guid}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] Guid moduloId, [FromRoute] Guid aulaId)
        {
            var aulaResult = await _aulaRepository.Delete(moduloId, aulaId);

            return aulaResult switch
            {
                AulaRepositoryResult.NotFound => new NotFoundResult(),
                _ => Ok()
            };
        }
    }
}