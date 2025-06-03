using AthletesApplication.Service.Implementation;
using AthletesApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AthletesApplication.Web.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ITournamentService _tournamensService;

        public TournamentsController(ITournamentService tournamensService)
        {
            _tournamensService = tournamensService;
        }

        // POST: Tournaments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create()
        {
            // Implement method
            // 1. Get current user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // 2. Call method CreateTournament from _tournamensService
            var tournament = _tournamensService.CreateTournament(userId);
            // 3. Redirect to Details
            return RedirectToAction("Details");
        }

        // GET: Tournaments/Details/5
        // Bonus task
        public IActionResult Details(Guid id)
        {
            // TODO: Implement method
            // Call service method, return view with tournament
            return View();
            //throw new NotImplementedException();
        }
    }
}
