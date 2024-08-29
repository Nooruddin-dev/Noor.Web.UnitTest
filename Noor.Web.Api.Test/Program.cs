using Noor.Web.Api.Test;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        #region minimal apis
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var students = new[]
        {
            new Student(1, "Alice"),
            new Student(2, "Bob"),
            new Student(3, "Charlie"),
            new Student(4, "Diana")
        };

        // Weather api end point
        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");

        // Student API endpoint
        app.MapGet("/students", () =>
        {
            return students;
        })
        .WithName("GetStudents");

        // Get student by ID API endpoint
        app.MapGet("/students/{id:int}", (int id) =>
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            return student is not null ? Results.Ok(student) : Results.NotFound();
        })
        .WithName("GetStudentById");

        #endregion

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}

// Record type for the WeatherForecast
internal record WeatherForecast(DateTime Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Record type for Student
internal record Student(int Id, string Name);
