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
    public class InterviewsController : Controller
    {
        private readonly IInterviewService _interviewService;
        private readonly IJobPositionService _jobPositionService;

        public InterviewsController(IInterviewService interviewService, IJobPositionService jobPositionService)
        {
            _interviewService = interviewService;
            _jobPositionService = jobPositionService;
        }

        // GET: Interviews
        public async Task<IActionResult> Index()
        {
            var interviews = _interviewService.GetAll();
            return View(interviews);
        }

        // GET: Interviews/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = _interviewService.GetById(id.Value);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }

        // GET: Interviews/Create
        public IActionResult Create()
        {
            var jobPositions = _jobPositionService.GetAll();
            ViewData["JobPositionId"] = new SelectList(jobPositions, "Id", "Department");
            return View();
        }

        // POST: Interviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InterviewDate,InterviewType,Notes,JobPositionId")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                interview.Id = Guid.NewGuid();
                _interviewService.Insert(interview);
                return RedirectToAction(nameof(Index));
            }
            var jobPositions = _jobPositionService.GetAll();
            ViewData["JobPositionId"] = new SelectList(jobPositions, "Id", "Department", interview.JobPositionId);
            return View(interview);
        }

        // GET: Interviews/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = _interviewService.GetById(id.Value);
            if (interview == null)
            {
                return NotFound();
            }
            var jobPositions = _jobPositionService.GetAll();
            ViewData["JobPositionId"] = new SelectList(jobPositions, "Id", "Department", interview.JobPositionId);
            return View(interview);
        }

        // POST: Interviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,InterviewDate,InterviewType,Notes,JobPositionId")] Interview interview)
        {
            if (id != interview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _interviewService.Update(interview);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewExists(interview.Id))
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
            var jobPositions = _jobPositionService.GetAll();
            ViewData["JobPositionId"] = new SelectList(jobPositions, "Id", "Department", interview.JobPositionId);
            return View(interview);
        }

        // GET: Interviews/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = _interviewService.GetById(id.Value);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }

        // POST: Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _interviewService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewExists(Guid id)
        {
            var interview = _interviewService.GetById(id);
            return interview != null;
        }
    }
}
