using MuseumApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Service.Interface
{
    public interface IVisitorService
    {
        List<Visitor> GetAll();
        Visitor? GetById(Guid id);
        Visitor Insert(Visitor visitor);
        Visitor Update(Visitor visitor);
        Visitor DeleteById(Guid id);
    }
}
