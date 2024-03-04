using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zajecia3_2.Data;
using Zajecia3_2.Models;

namespace Zajecia3_2.Controllers
{
    [Authorize] // Zabezpiecza dostęp tylko dla zalogowanych użytkowników
    public class OrderController : Controller
    {
        private readonly MvcTextContext _context;

        public OrderController(MvcTextContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Finalize()
        {
            var rentedMovies = await _context.Movie.Where(m => m.IsRented).ToListAsync();

            var viewModel = new OrderViewModel
            {
                RentedMovies = rentedMovies.Select(m => new OrderItemViewModel
                {
                    Title = m.Title,
                    Price = m.Price
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int movieIndex)
        {
            var rentedMovies = await _context.Movie.Where(m => m.IsRented).ToListAsync();

            if (movieIndex >= 0 && movieIndex < rentedMovies.Count)
            {
                var movieToRemove = rentedMovies[movieIndex];
                movieToRemove.IsRented = false;
                _context.Update(movieToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Finalize));
        }

        public IActionResult Confirmed()
        {
            var rentedMovies = _context.Movie.Where(m => m.IsRented).ToList();
           
                // Clear the rented movies in the cart
                foreach (var movie in rentedMovies)
                {
                    movie.IsRented = false;
                    _context.Update(movie);
                }

                _context.SaveChanges();
            
            return View();
        }
    }
}
