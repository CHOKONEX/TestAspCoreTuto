﻿using App.Core.Dto.Tests;
using App.Core.Infra.Repositories.Databases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
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
        private readonly IDapperArchiveRepository _dapperArchiveRepository;
        private readonly IDataProtector _protector;

        public MoviesController(IDapperMovieRepository movieRepository, ICustomerDapperTypeRepository customerDapperTypeRepository,
            IDapperArchiveRepository dapperArchiveRepository, IDataProtectionProvider provider)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _customerDapperTypeRepository = customerDapperTypeRepository;
            _dapperArchiveRepository = dapperArchiveRepository ?? throw new ArgumentNullException(nameof(dapperArchiveRepository));

            _protector = provider.CreateProtector("string to protect");
            string crypted = _protector.Protect("plain password");
            Console.WriteLine($"value protected = {crypted} /unprotect = {_protector.Unprotect(crypted)}");
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

        [HttpGet("count")]
        public async Task<IActionResult> GetAllDirectorsTotalMovies()
        {
            return Ok(await _movieRepository.GetAllDirectorsTotalMovies());
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

        [HttpGet("update")]
        public async Task<IActionResult> UpdateMovie()
        {
            int result = await _movieRepository.UpdateMovie();
            if (result > 0)
            {
                return Ok(new { Message = "Movie updated successfully." });
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _customerDapperTypeRepository.GetAllCustomers());
        }

        [HttpGet("archive")]
        public async Task<IActionResult> Archive_Table_Movies()
        {
            await _dapperArchiveRepository.Archive_Table_Movies();
            return Ok();
        }
    }
}