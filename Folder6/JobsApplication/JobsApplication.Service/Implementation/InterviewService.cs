using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace JobsApplication.Service.Implementation
{
    public class InterviewService : IInterviewService
    {
        private readonly IRepository<Interview> _interviewRepository;

        public InterviewService(IRepository<Interview> interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public List<Interview> GetAll()
        {
            return _interviewRepository.GetAll(selector: i => i, include: i => i.Include(y => y.JobPosition)).ToList();
        }

        public Interview? GetById(Guid id)
        {
            return _interviewRepository.Get(selector: i => i, predicate: i => i.Id.Equals(id), include: i => i.Include(y => y.JobPosition));
        }

        public Interview Insert(Interview interview)
        {
            _interviewRepository.Insert(interview);
            return interview;
        }

        public Interview Update(Interview interview)
        {
            _interviewRepository.Update(interview);
            return interview;
        }

        public Interview DeleteById(Guid id)
        {
            var entity = GetById(id);
            if (entity == null) throw new Exception("Interview not found");

            _interviewRepository.Delete(entity);
            return entity;
        }
    }
}