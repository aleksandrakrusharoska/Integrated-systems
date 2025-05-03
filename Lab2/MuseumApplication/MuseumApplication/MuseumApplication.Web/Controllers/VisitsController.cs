using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuseumApplication.Domain.DomainModels;
using MuseumApplication.Repository.Data;

namespace MuseumApplication.Web.Controllers
{
    public class VisitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visits/Create
        // TODO: Add the VisitorId as parameter and use it in the view as a value for the hidden input
        // You can make a separate ViewModel or send the parameter via ViewData
        // Use the SelectList to populate the drop-down list in the view
        // Replace the usage of ApplicationDbContext with the appropriate service
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // TODO: Bind the form from the view to this POST action in order to create the Visit
        // Implement the IVisitService and use it here to create the visit
        // After successful creation, the user should be redirected to Index page of Visitors

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VisitorId,ArtifactId,DateVisited")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                visit.Id = Guid.NewGuid();
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtifactId"] = new SelectList(_context.Artifacts, "Id", "Description", visit.ArtifactId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Email", visit.VisitorId);
            return View(visit);
        }
    }
}
