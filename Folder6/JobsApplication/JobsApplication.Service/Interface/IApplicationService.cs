using JobsApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Service.Interface
{
    public interface IApplicationService
    {
       

        public Application ScheduleInterviewForCandidateAndPosition(Guid candidateId, Guid positionId);
    }
}
