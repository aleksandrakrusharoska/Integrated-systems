using JobsApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Service.Interface
{
    public interface IJobPositionService
    {
        List<JobPosition> GetAll();
        JobPosition? GetById(Guid id);
        JobPosition Insert(JobPosition jobPosition);
        JobPosition Update(JobPosition jobPosition);
        JobPosition DeleteById(Guid id);
    }
}
