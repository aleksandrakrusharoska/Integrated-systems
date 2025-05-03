using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;

namespace JobsApplication.Service.Implementation
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<Application> _applicationRepository;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public ApplicationService(IRepository<Application> applicationRepository,
            IRepository<Candidate> candidateRepository, IRepository<JobPosition> jobPositionRepository)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
            _jobPositionRepository = jobPositionRepository;
        }

        public Application ScheduleInterviewForCandidateAndPosition(Guid candidateId, Guid positionId)
        {
            var candidate = _candidateRepository.Get(c => c, c => c.Id.Equals(candidateId));
            var jobPosition = _jobPositionRepository.Get(c => c, c => c.Id.Equals(positionId));

            if (candidate == null) throw new Exception("Candidate not found");
            if (jobPosition == null) throw new Exception("Job Position not found");

            var application = new Application
            {
                Id = Guid.NewGuid(),
                CandidateId = candidateId,
                Candidate = candidate,
                JobPositionId = positionId,
                JobPosition = jobPosition,
                Status = "Applied",
                AppliedDate = DateTime.UtcNow
            };

            _applicationRepository.Insert(application);
            return application;
        }
    }
}