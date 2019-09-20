using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenSpace.Models;
using OpenSpace.Request;
using OpenSpace.Sevices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSpace.Controllers
{
    //Passo 9
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        public readonly IActorService actorService;

        public ActorsController(IActorService actorService)
        {
            this.actorService = actorService;
        }

        // GET api/actors
        /// <summary>
        /// Lista todos os atores
        /// </summary>
        /// <returns>Lista todos os atores</returns>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        public ActionResult<List<Actor>> Get()
        {
            return actorService.GetAll();
        }

        // GET api/actors/5
        /// <summary>
        /// Procura um ator por meio do id
        /// </summary>
        /// <param name="id">Id do ator</param>
        /// <returns>ator de acordo com o id</returns>
        /// <response code="404">ator não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet("{id}")]
        public ActionResult<Actor> Get(int id)
        {
            try
            {
                return actorService.Get(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/actors
        /// <summary>
        /// Cria um novo ator
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/actors
        ///     {
        ///        
        ///     }
        /// </remarks>
        /// <param name="request">Dados do novo ator</param>
        /// <returns>Id do ator inserido</returns>
        /// <response code="400">Dados do modelo nulos ou inválidos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost]
        public ActionResult<int> Post(ActorRequest request)
        {
            try
            {
                int newactorId = actorService.Post(request);
                return Ok(newactorId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/actors/5
        /// <summary>
        /// Edita um ator
        /// </summary>
        /// <param name="id">Id do ator a ser editado</param>
        /// <param name="request">Dados do ator editado</param>
        /// <response code="400">Dados do modelo nulos ou inválidos</response>
        /// <response code="404">ator não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, ActorRequest request)
        {
            try
            {

                actorService.Put(id, request);
                return Ok("ator editado com sucesso");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/actors/5
        /// <summary>
        /// Excluir um ator
        /// </summary>
        /// <param name="id">Id do ator</param>
        /// <response code="404">ator não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            try
            {
                actorService.Delete(id);
                return Ok("ator deletado com sucesso");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
