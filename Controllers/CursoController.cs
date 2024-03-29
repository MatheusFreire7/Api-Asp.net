using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEscola_API.Data;
using ProjetoEscola_API.Models;
namespace ProjetoEscola_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
         private EscolaContext _context;

        public CursoController(EscolaContext context)
        {
            // construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<CursoEscola>> GetAll() 
        {
            return _context.CursoEscola.ToList();
        }

         [HttpGet("{CursoId}")]
        public ActionResult<List<CursoEscola>> Get(int CursoId)
        {
            try
            {
                var result = _context.CursoEscola.Find(CursoId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> post(CursoEscola model)
        {
        try
        {
            _context.CursoEscola.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                //return Ok();
                return Created($"/api/curso/{model.codCurso}",model);
            }
        }
        catch
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
        }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }

        [HttpPut("{CursoId}")]
        public async Task<IActionResult> put(int CursoId, CursoEscola dadosCursoAlt)
        {
            try 
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.CursoEscola.FindAsync(CursoId);
            
                result.nome = dadosCursoAlt.nome;
                result.codCurso = dadosCursoAlt.codCurso;
                await _context.SaveChangesAsync();
                return Created($"/api/curso/{dadosCursoAlt.codCurso}", dadosCursoAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falhano acesso ao banco de dados.");
            }
        }

        [HttpDelete("{CursoId}")]
        public async Task<ActionResult> delete(int CursoId)
        {
            try
            {
                //verifica se existe aluno a ser excluído
                var curso = await _context.CursoEscola.FindAsync(CursoId);
                if (curso == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(curso);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falhano acesso ao banco de dados.");
            }
        }
    }
}