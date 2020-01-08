using LeaseFilms.Models;
using LeaseFilms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LeaseFilms.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public async Task<ActionResult> Index()
        {
            var movies = await _context.Movies.Include(m => m.Genre).ToListAsync();

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List", movies);

            return View("ReadOnlyList", movies);
        }

        public async Task<ActionResult> Details(int id)
        {
            var movie = await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<ActionResult> New()
        {
            var genres = await _context.Genres.ToListAsync();

            var modelView = new MovieFormViewModel
            {
                Genres = genres,
                //Movie = new Movie()
            };

            return View("MovieForm", modelView);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<ActionResult> Edit(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = await _context.Genres.ToListAsync(),
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<ActionResult> Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = await _context.Genres.ToListAsync()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = await _context.Movies.SingleAsync(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Movies");
        }
    }
}