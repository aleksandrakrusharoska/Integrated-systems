using MuseumApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Service.Interface
{
    public interface ICollectionService
    {
        List<Collection> GetAll();
        Collection? GetById(Guid id);
        Collection Insert(Collection collection);
        Collection Update(Collection collection);
        Collection DeleteById(Guid id);
    }
}
