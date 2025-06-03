using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoursesApplication.Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> GetAll()
        {
            return _courseRepository
                .GetAll(selector: c => c)
                .ToList();
        }

        public Course? GetById(Guid id)
        {
            return _courseRepository
                .Get(selector: c => c, 
                    predicate: c => c.Id.Equals(id), include: c => 
                        c.Include(s => s.EnrolledStudents)
                            .ThenInclude(e => e.Student)
                            .Include(s => s.EnrolledStudents)
                            .ThenInclude(e => e.Semester)
                        );
        }

        public Course Insert(Course course)
        {
            return _courseRepository.Insert(course);
        }

        public ICollection<Course> InsertMany(ICollection<Course> courses)
        {
            return _courseRepository.InsertMany(courses);
        }

        public Course Update(Course course)
        {
            return _courseRepository.Update(course);
        }

        public Course DeleteById(Guid id)
        {
            var course = this.GetById(id);

            if (course == null)
            {
                throw new Exception($"Course with ID {id} not found.");
            }

            return _courseRepository.Delete(course);
        }
    }
}
