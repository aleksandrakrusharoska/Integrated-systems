using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;

namespace JobsApplication.Service.Implementation
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public JobPositionService(IRepository<JobPosition> jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        public List<JobPosition> GetAll()
        {
            return _jobPositionRepository.GetAll(selector: jp => jp).ToList();
        }

        public JobPosition? GetById(Guid id)
        {
            return _jobPositionRepository.Get(selector: jp => jp, predicate: jp => jp.Id.Equals(id));
        }

        public JobPosition Insert(JobPosition jobPosition)
        {
            _jobPositionRepository.Insert(jobPosition);
            return jobPosition;
        }

        public JobPosition Update(JobPosition jobPosition)
        {
            _jobPositionRepository.Update(jobPosition);
            return jobPosition;
        }

        public JobPosition DeleteById(Guid id)
        {
            var entity = GetById(id);

            if (entity == null) throw new Exception("JobPosition not found");

            _jobPositionRepository.Delete(entity);
            return entity;
        }
    }
}