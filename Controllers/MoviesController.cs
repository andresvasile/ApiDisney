using ApiDisney.Dto;
using ApiDisney.Errors;
using ApiDisney.Models;
using ApiDisney.Specifications;
using AutoMapper;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDisney.Controllers
{
    public class MoviesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MoviesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MovieSpecificDto>>> GetMovies()
        {

            var movies = await _unitOfWork.Repository<Movie>().ListAllAsync();
            return _mapper.Map<List<Movie>, List<MovieSpecificDto>>(movies.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMoviesWithMovies(int id)
        {

            var spec = new ExistingMovieByIdSpecification(id);
            var movie = await _unitOfWork.Repository<Movie>().GetEntityWithSpec(spec);


            return _mapper.Map<Movie, MovieDto>(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(MovieDto movieDto)
        {
            var spec = new ExistingMovieByTitleSpecification(movieDto.Title);

            var movieValidate = await _unitOfWork.Repository<Movie>().GetEntityWithSpec(spec);

            if (movieValidate != null)
            {
                return BadRequest(new ApiResponse(400, "Movie already exists"));
            }

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);

            _unitOfWork.Repository<Movie>().Add(movie);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return movieDto;

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDto>> UpdateMovie(int id, [FromBody] MovieDto movieDto)
        {
            var spec = new ExistingMovieByIdSpecification(id);

            var movie = await _unitOfWork.Repository<Movie>().GetEntityWithSpec(spec);

            if (movie == null)
            {
                return BadRequest(new ApiResponse(404, "Movie not found"));
            }

            var specName = new ExistingMovieByTitleSpecification(movieDto.Title);

            var movieValidate = await _unitOfWork.Repository<Movie>().GetEntityWithSpec(specName);

            if (movieValidate != null && movie.Id_Movie != movieValidate.Id_Movie)
            {
                return BadRequest(new ApiResponse(400, "Movie name already exists"));
            }

            movie.Title = movieDto.Title ?? movie.Title;
            movie.Rating = movieDto.Rating < 0 ? movie.Rating : movieDto.Rating;
            movie.Id_Genre = movieDto.Id_Genre < 0 ? movie.Id_Genre : movieDto.Id_Genre;
            movie.Image = movieDto.Image ?? movie.Image;

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            var movieDtoToReturn = _mapper.Map<Movie, MovieDto>(movie);

            return movieDtoToReturn;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var spec = new ExistingMovieByIdSpecification(id);

            var movie = await _unitOfWork.Repository<Movie>().GetEntityWithSpec(spec);

            if (movie == null)
            {
                return BadRequest(new ApiResponse(404, "Movie not found"));
            }

            _unitOfWork.Repository<Movie>().Delete(movie);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return Ok($" Movie {movie.Title} was deleted");
        }

        [HttpGet("name/{title}")]
        public async Task<ActionResult<IReadOnlyList<MovieDto>>> GetMovieByNombre(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var spec = new ExistingMovieByTitleSpecification(title);
                var moviesWithNameSpecific = await _unitOfWork.Repository<Movie>().ListAsync(spec);

                if (moviesWithNameSpecific.Count > 0)
                {
                    return _mapper.Map<List<Movie>, List<MovieDto>>(moviesWithNameSpecific.ToList());
                }
                return Ok("No se encontraron resultados con ese titulo");

            }

            return BadRequest(new ApiResponse(400, "No ingreso ningun filtro"));
        }

        [HttpGet("genre/{idGenero}")]
        public async Task<ActionResult<IReadOnlyList<MovieDto>>> GetMovieByGenre(int idGenero)
        {

            var spec = new ExistingMovieByIdGenre(idGenero);
            var moviesWithGenreSpecific = await _unitOfWork.Repository<Movie>().ListAsync(spec);

            if (moviesWithGenreSpecific.Count > 0)
            {
                return _mapper.Map<List<Movie>, List<MovieDto>>(moviesWithGenreSpecific.ToList());
            }
            return Ok("No se encontraron resultados con ese genero");

        }

        [HttpGet("order/{order}")]
        public async Task<ActionResult<IReadOnlyList<MovieDto>>> GetMoviesByOrder(string order)
        {

            var spec = new ExistingMoviesByOrder(order);
            var moviesWithGenreSpecific = await _unitOfWork.Repository<Movie>().ListAsync(spec);

            if (moviesWithGenreSpecific.Count > 0)
            {
                return _mapper.Map<List<Movie>, List<MovieDto>>(moviesWithGenreSpecific.ToList());
            }
            return Ok("No se encontraron resultados con ese genero");

        }
    }
}