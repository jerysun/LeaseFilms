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
    public class NewRentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateNewRentals(NewRentalDto newRental)
        {
            if (newRental.MovieIds.Count == 0)
                return BadRequest("No Movie Ids are given.");

            Customer customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == newRental.CustomerId);
            if (customer == null)
                return BadRequest("CustomerId is invalid");

            var movies = await _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToListAsync();
            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more MovieIds are invalid");


            foreach (Movie movie in movies)
            {
                if (movie.NumberAvailable > 0)
                    return BadRequest("Movie is unavailable");

                --movie.NumberAvailable;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                };

                _context.Rentals.Add(rental);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
