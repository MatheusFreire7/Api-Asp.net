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

    public class DisciplinaController : ControllerBase
    {
         private EscolaContext _context;

        public DisciplinaController(EscolaContext context)
        {
            // construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Disciplina>> GetAll() 
        {
            return _context.Disciplina.ToList();
        }

        [HttpGet("{DisciplinaId}")]
        public ActionResult<List<Disciplina>> Get(int DisciplinaId)
        {
            try
            {
                var result = _context.Disciplina.Find(DisciplinaId);
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
        public async Task<ActionResult> post(Disciplina model)
        {
        try
        {
            if(model.ano > 2023 || model.ano < 1)
            {
                return BadRequest();
            }
            _context.Disciplina.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                //return Ok();
                return Created($"/api/disciplina/{model.id}",model);
            }
        }
        catch
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
        }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }

        [HttpPut("{DisciplinaId}")]
        public async Task<IActionResult> put(int DisciplinaId, Disciplina dadosDisciplinaAlt)
        {
            try 
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Disciplina.FindAsync(DisciplinaId);
                if (DisciplinaId != result.id || dadosDisciplinaAlt.ano > 2023 || dadosDisciplinaAlt.ano < 1)
                {
                    return BadRequest();
                }
                result.nome = dadosDisciplinaAlt.nome;
                result.curso = dadosDisciplinaAlt.curso;
                result.ano = dadosDisciplinaAlt.ano;
              
                await _context.SaveChangesAsync();
                return Created($"/api/discpina/{dadosDisciplinaAlt.id}", dadosDisciplinaAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falhano acesso ao banco de dados.");
            }
        }

        [HttpDelete("{DisciplinaId}")]
        public async Task<ActionResult> delete(int DisciplinaId)
        {
            try
            {
                //verifica se existe aluno a ser excluído
                var disciplina = await _context.Disciplina.FindAsync(DisciplinaId);
                if (disciplina == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(disciplina);
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