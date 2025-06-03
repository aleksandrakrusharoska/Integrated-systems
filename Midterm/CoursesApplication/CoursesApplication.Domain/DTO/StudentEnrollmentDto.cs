using CoursesApplication.Domain.DomainModels;

namespace CoursesApplication.Domain.DTO
{
    public class StudentEnrollmentDto
    {
        public Guid CourseId { get; set; }

        public Guid SemesterId { get; set; }

        public List<Semester> Semesters { get; set; } = new();

        public bool ReEnroll { get; set; } = false;
    }
}