using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RKC.Cursos.Modulos.Dtos;
using RKC.Cursos.Modulos.Enums;
using RKC.Cursos.Modulos.Services;

namespace RKC.Cursos.Modulos.Controllers
{
    [Route("cursos/modulos")]
    public class ModuloController : ControllerBase
    {
        private readonly IModuloRepositoryService _moduloRepository;

        public ModuloController(IModuloRepositoryService moduloRepository)
        {
            _moduloRepository = moduloRepository;
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
            return Ok(modulo);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ModuloOutput>>> GetList([FromQuery] string nomeFilter)
        {
            return Ok(await _moduloRepository.GetList(nomeFilter));
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