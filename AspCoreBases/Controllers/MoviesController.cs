using App.Core.Dto.Tests;
using App.Core.Infra.Repositories.Databases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TestAspCoreTuto.Controllers
{
    [ApiController]
    [Route("test/movies")]
    [AllowAnonymous]
    public class MoviesController : ControllerBase
    {
        private readonly IDapperMovieRepository _movieRepository;
        private readonly ICustomerDapperTypeRepository _customerDapperTypeRepository;

        public MoviesController(IDapperMovieRepository movieRepository, ICustomerDapperTypeRepository customerDapperTypeRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _customerDapperTypeRepository = customerDapperTypeRepository;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllMovies()
        {
            return Ok(await _movieRepository.GetAllMovies());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovies(int id)
        {
            MovieModel movie = await _movieRepository.GetMovieById(id);
            if (movie != null)
            {
                return Ok(movie);
            }

            return NotFound(new { Message = $"Movie with id {id} is not available." });
        }

        [HttpGet("director/{id}")]
        public async Task<IActionResult> GetMoviesGetDirectorAndMovies(int id)
        {
            DirectorModel director = await _movieRepository.GetDirectorAndMovies(id);
            if (director != null)
            {
                return Ok(director);
            }

            return NotFound(new { Message = $"Director with id {id} is not available." });
        }

        [HttpGet("alldirectors")]
        public async Task<IActionResult> GetAllDirectors()
        {
            return Ok(await _movieRepository.GetAllDirectors());
        }

        [HttpGet("directors")]
        public async Task<IActionResult> GetDirectors()
        {
            return Ok(await _movieRepository.GetDirectors());
        }

        [HttpGet("identities")]
        public async Task<IActionResult> GetDirectorsIdentities()
        {
            return Ok(await _movieRepository.GetDirectorsIdentities());
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMovie(CreateMovieModel model)
        {
            int result = await _movieRepository.AddMovie(model);
            if (result > 0)
            {
                return Ok(new { Message = "Movie added successfully." });
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _customerDapperTypeRepository.GetAllCustomers());
        }
    }
}