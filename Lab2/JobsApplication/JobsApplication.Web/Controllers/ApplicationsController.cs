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
using Application = JobsApplication.Domain.DomainModels.Application;
using JobsApplication.Domain.Dtos;

namespace JobsApplication.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly ICandidateService _candidateService;
        private readonly IJobPositionService _jobPositionService;

        public ApplicationsController(IApplicationService applicationService, ICandidateService candidateService, IJobPositionService jobPositionService)
        {
            _candidateService = candidateService;
            _jobPositionService = jobPositionService;
            _applicationService = applicationService;
        }


        // GET: Applications/Create
        // TODO: Add the CandidateId as parameter and use it in the view as a value for the hidden input
        // You can make a separate ViewModel or send the parameter via ViewData/ViewBag
        // Use the SelectList to populate the drop-down list in the view
        // Replace the usage of ApplicationDbContext with the appropriate service
        public IActionResult Create(Guid candidateId)
        {
            //var candidates = _candidateService.GetAll();
            var jobPositions = _jobPositionService.GetAll();
            //ViewData["CandidateId"] = new SelectList(candidates, "Id", "Email");
            ViewData["CandidateId"] = candidateId;
            ViewData["JobPositionId"] = new SelectList(jobPositions, "Id", "Department");

            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // TODO: Bind the form from the view to this POST action in order to create the Application
        // Implement the IApplicationService and use it here to create the visit
        // After successful creation, the user should be redirected to Index page of Candidates

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateApplicationDto application)
        {
            if (ModelState.IsValid)
            {
                _applicationService.ScheduleInterviewForCandidateAndPosition(application.CandidateId, application.JobPositionId);
                return RedirectToAction(nameof(Index), "Candidates");
            }

            var candidates = _candidateService.GetAll();
            var jobPositions = _jobPositionService.GetAll();
            ViewData["CandidateId"] = new SelectList(candidates, "Id", "Email", application.CandidateId);
            ViewData["JobPositionId"] = new SelectList(jobPositions, "Id", "Department", application.JobPositionId);
            return View(application);
        }

    }
}
