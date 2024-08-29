
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;

namespace Noor.XUnit.TestCases
{
    public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

       

        [Fact]
        public async Task GetWeatherForecast_ReturnsExpectedForecast()
        {
            // Act
            var response = await _client.GetAsync("/weatherforecast");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var forecast = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
            forecast.Should().NotBeNull();
            forecast.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetStudents_ReturnsAllStudents()
        {
            // Arrange
            var url = "/students";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var students = await response.Content.ReadFromJsonAsync<Student[]>();
            students.Should().NotBeNull();
            students.Should().HaveCount(4);
        }

        [Theory]
        [InlineData(1, HttpStatusCode.OK)]
        [InlineData(2, HttpStatusCode.OK)]
        [InlineData(99, HttpStatusCode.NotFound)] // Non-existent ID
        public async Task GetStudentById_ReturnsExpectedResult(int id, HttpStatusCode expectedStatusCode)
        {
            // Act
            var response = await _client.GetAsync($"/students/{id}");

            // Assert
            response.StatusCode.Should().Be(expectedStatusCode);

            if (expectedStatusCode == HttpStatusCode.OK)
            {
                var student = await response.Content.ReadFromJsonAsync<Student>();
                student.Should().NotBeNull();
                student.Id.Should().Be(id);
            }
        }
    }

    // Model classes for deserialization
    public record WeatherForecast(DateTime Date, int TemperatureC, string Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public record Student(int Id, string Name);
}
