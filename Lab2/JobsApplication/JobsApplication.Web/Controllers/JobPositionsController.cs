using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Data;
using JobsApplication.Service.Interface;

namespace JobsApplication.Web.Controllers
{
    public class JobPositionsController : Controller
    {
        private readonly IJobPositionService _jobPositionService;

        public JobPositionsController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        // GET: JobPositions
        public async Task<IActionResult> Index()
        {
            var jobPositions = _jobPositionService.GetAll();
            return View(jobPositions);
        }

        // GET: JobPositions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = _jobPositionService.GetById(id.Value);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // GET: JobPositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Department,Location")] JobPosition jobPosition)
        {
            if (ModelState.IsValid)
            {
                jobPosition.Id = Guid.NewGuid();
                _jobPositionService.Insert(jobPosition);
                return RedirectToAction(nameof(Index));
            }
            return View(jobPosition);
        }

        // GET: JobPositions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = _jobPositionService.GetById(id.Value);
            if (jobPosition == null)
            {
                return NotFound();
            }
            return View(jobPosition);
        }

        // POST: JobPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Department,Location")] JobPosition jobPosition)
        {
            if (id != jobPosition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _jobPositionService.Update(jobPosition);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPositionExists(jobPosition.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobPosition);
        }

        // GET: JobPositions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = _jobPositionService.GetById(id.Value);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // POST: JobPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _jobPositionService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool JobPositionExists(Guid id)
        {
            var jobPosition = _jobPositionService.GetById(id);
            return jobPosition != null;
        }
    }
}
