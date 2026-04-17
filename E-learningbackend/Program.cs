using Elearningbackend.Data;
using Elearningbackend.Models;
using Elearningbackend.Repositories;
using Elearningbackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Add DbContext
builder.Services.AddDbContext<ElearningDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();

// Add Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IQuizService, QuizService>();

// Add Controllers
builder.Services.AddControllers();
// Add Swagger
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Apply migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ElearningDbContext>();
    dbContext.Database.Migrate();

    // Seed sample data if database is empty
    if (!dbContext.Users.Any())
    {
        var users = new List<User>
        {
            new User
            {
                FullName = "John Doe",
                Email = "john@example.com",
                PasswordHash = "hashed_password_1",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                FullName = "Jane Smith",
                Email = "jane@example.com",
                PasswordHash = "hashed_password_2",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                FullName = "Mike Johnson",
                Email = "mike@example.com",
                PasswordHash = "hashed_password_3",
                CreatedAt = DateTime.UtcNow
            }
        };
        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();

        var courses = new List<Course>
        {
            new Course
            {
                Title = "Introduction to C#",
                Description = "Learn the basics of C# programming language",
                CreatedBy = users[0].UserId,
                CreatedAt = DateTime.UtcNow
            },
            new Course
            {
                Title = "Web Development with ASP.NET Core",
                Description = "Build modern web applications using ASP.NET Core",
                CreatedBy = users[0].UserId,
                CreatedAt = DateTime.UtcNow
            },
            new Course
            {
                Title = "Database Design",
                Description = "Master database design principles and SQL",
                CreatedBy = users[1].UserId,
                CreatedAt = DateTime.UtcNow
            },
            new Course
            {
                Title = "Advanced JavaScript",
                Description = "Deep dive into JavaScript and async programming",
                CreatedBy = users[1].UserId,
                CreatedAt = DateTime.UtcNow
            },
            new Course
            {
                Title = "Cloud Computing with Azure",
                Description = "Deploy and manage applications on Microsoft Azure",
                CreatedBy = users[2].UserId,
                CreatedAt = DateTime.UtcNow
            }
        };
        dbContext.Courses.AddRange(courses);
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Redirect root URL to Swagger UI
    app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
