using JobsApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Service.Interface
{
    public interface IInterviewService
    {
        List<Interview> GetAll();
        Interview? GetById(Guid id);
        Interview Insert(Interview interview);
        Interview Update(Interview interview);
        Interview DeleteById(Guid id);
    }
}
