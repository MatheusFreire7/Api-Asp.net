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
    public class ProfessorController : ControllerBase
    {
         private EscolaContext _context;

        public ProfessorController(EscolaContext context)
        {
            // construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Professor>> GetAll() 
        {
            return _context.Professor.ToList();
        }

        [HttpGet("{ProfessorId}")]
        public ActionResult<List<Professor>> Get(int ProfessorId)
        {
            try
            {
                var result = _context.Professor.Find(ProfessorId);
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
        public async Task<ActionResult> post(Professor model)
        {
        try
        {
            _context.Professor.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                //return Ok();
                return Created($"/api/professor/{model.id}",model);
            }
        }
        catch
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
        }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }

        [HttpPut("{ProfessorId}")]
        public async Task<IActionResult> put(int ProfessorId, Professor dadosProfessorAlt)
        {
            try 
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Professor.FindAsync(ProfessorId);
                if (ProfessorId != result.id)
                {
                    return BadRequest();
                }
                result.nome = dadosProfessorAlt.nome;
                result.email = dadosProfessorAlt.email;
                result.telefone = dadosProfessorAlt.telefone;
                result.data_nascimento = dadosProfessorAlt.data_nascimento;
                result.disciplina = dadosProfessorAlt.disciplina;
                await _context.SaveChangesAsync();
                return Created($"/api/professor/{dadosProfessorAlt.id}", dadosProfessorAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falhano acesso ao banco de dados.");
            }
        }

        [HttpDelete("{ProfessorId}")]
        public async Task<ActionResult> delete(int ProfessorId)
        {
            try
            {
                //verifica se existe aluno a ser excluído
                var professor = await _context.Professor.FindAsync(ProfessorId);
                if (professor == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(professor);
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