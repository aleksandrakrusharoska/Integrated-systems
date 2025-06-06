﻿namespace CoursesApplication.Domain.DTO
{
    public class FetchCoursesDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int ECTS { get; set; } = 0;

        public string SemesterType { get; set; } = "WINTER";
    }
}