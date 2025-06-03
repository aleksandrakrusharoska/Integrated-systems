using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using CoursesApplication.Domain.DTO;
using CoursesApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CoursesApplication.Web.Controllers
{
    [Authorize]
    public class StudentOnCourseController : Controller
    {
        private readonly IStudentOnCourseService _studentOnCourseService;
        private readonly ISemesterService _semesterService;

        public StudentOnCourseController(IStudentOnCourseService studentOnCourseService, ISemesterService semesterService)
        {
            _studentOnCourseService = studentOnCourseService;
            _semesterService = semesterService;
        }


        // GET: StudentOnCourse
        // Enrolled Courses Page
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_studentOnCourseService.GetAllByStudentId(userId));
        }

        public IActionResult EnrollOnCourse(Guid courseId)
        {
            //TODO: This actions should return a View with a form to enroll on a course.
            // For the selected CourseId choose and select the semester from a dropdown.
            // You need to implement the logic to fetch the semesters and pass them to the ViewData for the dropdown.
            // Additionally the form should have a checkbox to re-enroll on the course if the student has already enrolled on it.
            
            //ViewBag.CourseId = courseId;
            //ViewBag.Semesters = new SelectList(_semesterService.GetAll(), "Id", "Name");

            var dto = new StudentEnrollmentDto
            {
                CourseId = courseId,
                Semesters = _semesterService.GetAll(),
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult SubmitCourseEnrollemnt(StudentEnrollmentDto dto)
        {
            // TODO: Implement the logic to enroll the logged in student on the course.
            // Then redirect to the Index action to show the enrolled courses.
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return Unauthorized();
            }

            _studentOnCourseService.EnrollOnCourse(user, dto.CourseId, dto.SemesterId, dto.ReEnroll);

            return RedirectToAction("Index");
        }
    }
}
