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
    public class NotaController : ControllerBase
    {
        private EscolaContext _context;

        public NotaController(EscolaContext context)
        {
            // construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Nota>> GetAll() 
        {
            return _context.Nota.ToList();
        }

        [HttpGet("{NotaId}")]
        public ActionResult<List<Nota>> Get(int NotaId)
        {
            try
            {
                var result = _context.Nota.Find(NotaId);
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
        public async Task<ActionResult> post(Nota model)
        {
        try
        {
            if(model.nota > 10  || model.nota < 0 || model.ra.Length > 5 )
            {
                return BadRequest();
            }
            _context.Nota.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                //return Ok();
                return Created($"/api/nota/{model.id}",model);
            }
        }
        catch
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
        }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }

        [HttpPut("{NotaId}")]
        public async Task<IActionResult> put(int NotaId, Nota dadosNotaAlt)
        {
            try 
            {
              
                var result = await _context.Nota.FindAsync(NotaId);
               
                if (NotaId != result.id || dadosNotaAlt.nota > 10 || dadosNotaAlt.nota < 0 || dadosNotaAlt.ra.Length > 5 )
                {
                    return BadRequest();
                }
                result.ra = dadosNotaAlt.ra;
                result.nota = dadosNotaAlt.nota;
                result.disciplina = dadosNotaAlt.disciplina;
                //result.disciplina = dadosNotaAlt.disciplina;
    
                await _context.SaveChangesAsync();
                return Created($"/api/nota/{dadosNotaAlt.ra}", dadosNotaAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falhano acesso ao banco de dados.");
            }
        }

        [HttpDelete("{NotaId}")]
        public async Task<ActionResult> delete(int NotaId)
        {
            try
            {
                //verifica se existe aluno a ser excluído
                var nota = await _context.Nota.FindAsync(NotaId);
                if (nota == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(nota);
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