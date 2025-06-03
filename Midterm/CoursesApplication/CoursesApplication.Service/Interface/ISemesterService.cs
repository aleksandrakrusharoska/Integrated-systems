using CoursesApplication.Domain.DomainModels;

namespace CoursesApplication.Service.Interface;

public interface ISemesterService
{
    List<Semester> GetAll();
    Semester? GetById(Guid id);
    Semester Insert(Semester flight);
    ICollection<Semester> InsertMany(ICollection<Semester> flights);
    Semester Update(Semester flight);
    Semester DeleteById(Guid id);
}