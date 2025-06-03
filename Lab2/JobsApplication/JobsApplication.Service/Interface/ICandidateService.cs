using JobsApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Service.Interface
{
    public interface ICandidateService
    {
        List<Candidate> GetAll();
        Candidate? GetById(Guid id);
        Candidate Insert(Candidate candidate);
        Candidate Update(Candidate candidate);
        Candidate DeleteById(Guid id);
    }
}
