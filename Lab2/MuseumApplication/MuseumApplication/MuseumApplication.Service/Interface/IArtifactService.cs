using MuseumApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Service.Interface
{
    internal interface IArtifactService
    {
        List<Artifact> GetAll();
        Artifact? GetById(Guid id);
        Artifact Insert(Artifact artifact);
        Artifact Update(Artifact artifact);
        Artifact DeleteById(Guid id);
    }
}
