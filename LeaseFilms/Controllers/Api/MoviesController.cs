using AutoMapper;
using LeaseFilms.Dtos;
using LeaseFilms.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LeaseFilms.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET /api/movies
        public async Task<IHttpActionResult> GetMovies()
        {
            var moviesInDb = await _context.Movies.Include(m => m.Genre).ToListAsync();
            var moviesDto = Mapper.Map<IEnumerable<MovieDto>>(moviesInDb);
            return Ok(moviesDto);
        }

        // GET /api/movies/1
        public async Task<IHttpActionResult> GetMovie(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return BadRequest();

            return Ok(Mapper.Map<MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        public async Task<IHttpActionResult> CreateMovie(MovieDto movieDto)
        {
            var movie = Mapper.Map<Movie>(movieDto);
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();
            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/1
        [HttpPut]
        public async Task<IHttpActionResult> UpdateMovie(int id, MovieDto movieDto)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return BadRequest();

            Mapper.Map(movieDto, movie);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE /api/movies/1
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return BadRequest();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
