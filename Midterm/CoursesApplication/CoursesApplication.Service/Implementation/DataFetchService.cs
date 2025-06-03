using System.Net.Http.Json;
using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Domain.DTO;
using CoursesApplication.Service.Interface;

namespace CoursesApplication.Service.Implementation
{
    public class DataFetchService : IDataFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly ICourseService _courseService;

        public DataFetchService(IHttpClientFactory httpClientFactory, ICourseService courseService)
        {
            _courseService = courseService;
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<List<Course>> FetchCoursesFromApi()
        {
            var url = "https://localhost:7245/api/courses";
            var response = await _httpClient.GetFromJsonAsync<List<FetchCoursesDto>>(url);
            
            if (response == null)
            {
                throw new Exception("Failed to fetch courses from API.");
            }

            var courses = response.Select(dto => new Course
            {
                Id = Guid.NewGuid(),
                Name = dto.Title,
                Credits = dto.ECTS,
                SemesterType = dto.SemesterType,
            }).ToList();
            return _courseService.InsertMany(courses).ToList();
        }
    }
}