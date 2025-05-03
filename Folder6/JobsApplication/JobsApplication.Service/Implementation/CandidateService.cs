using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;

namespace JobsApplication.Service.Implementation
{
    public class CandidateService : ICandidateService
    {
        private readonly IRepository<Candidate> _candidateRepository;

        public CandidateService(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public List<Candidate> GetAll()
        {
            return _candidateRepository.GetAll(selector: c => c).ToList();
        }

        public Candidate? GetById(Guid id)
        {
            return _candidateRepository.Get(selector: c => c, predicate: c => c.Id.Equals(id));
        }

        public Candidate Insert(Candidate candidate)
        {
            _candidateRepository.Insert(candidate);
            return candidate;
        }

        public Candidate Update(Candidate candidate)
        {
            _candidateRepository.Update(candidate);
            return candidate;
        }

        public Candidate DeleteById(Guid id)
        {
            var entity = GetById(id);

            if (entity == null) throw new Exception("Candidate not found");

            _candidateRepository.Delete(entity);
            return entity;
        }
    }
}