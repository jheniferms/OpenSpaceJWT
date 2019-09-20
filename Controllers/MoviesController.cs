using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenSpace.Models;
using OpenSpace.Request;
using OpenSpace.Sevices.Interface;
using System;
using System.Collections.Generic;

namespace OpenSpace.Controllers
{
    //Passo 7
    [Authorize(Roles ="User")]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        public readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        // GET api/movies
        /// <summary>
        /// Lista todos os Filmes
        /// </summary>
        /// <returns>Lista todos os Filmes</returns>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        public ActionResult<List<Movie>> Get()
        {
            return movieService.GetAll();
        }

        // GET api/movies/5
        /// <summary>
        /// Procura um filme por meio do id
        /// </summary>
        /// <param name="id">Id do Filme</param>
        /// <returns>Filme de acordo com o id</returns>
        /// <response code="404">Filme não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id)
        {
            try
            {
                return movieService.Get(id);
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

        // POST api/movies
        /// <summary>
        /// Cria um novo filme
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/movies
        ///     {
        ///        
        ///     }
        /// </remarks>
        /// <param name="request">Dados do novo filme</param>
        /// <returns>Id do filme inserido</returns>
        /// <response code="400">Dados do modelo nulos ou inválidos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost]
        public ActionResult<int> Post(MovieRequest request)
        {
            try
            {
                int newMovieId = movieService.Post(request);
                return Ok(newMovieId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/movies/5
        /// <summary>
        /// Edita um filme
        /// </summary>
        /// <param name="id">Id do filme a ser editado</param>
        /// <param name="request">Dados do filme editado</param>
        /// <response code="400">Dados do modelo nulos ou inválidos</response>
        /// <response code="404">filme não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, MovieRequest request)
        {
            try
            {

                movieService.Put(id, request);
                return Ok("Filme editado com sucesso");
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

        // DELETE api/movies/5
        /// <summary>
        /// Excluir um filme
        /// </summary>
        /// <param name="id">Id do filme</param>
        /// <response code="404">filme não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            try
            {
                movieService.Delete(id);
                return Ok("Filme deletado com sucesso");
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
