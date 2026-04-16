using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Elearningbackend.DTOs;

namespace Elearningbackend.Tests
{
    public class CoursesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CoursesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCourses_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/courses");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateCourse_ReturnsCreatedStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var createCourseDto = new CreateCourseDto
            {
                Title = "Test Course",
                Description = "Test Description"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/courses", createCourseDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}