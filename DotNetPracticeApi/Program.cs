using DotNetPracticeApi.Data;
using DotNetPracticeApi.Repositories;
using DotNetPracticeApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers(); //to register controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DB connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention()
);

// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Swagger
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
app.MapControllers(); // Map controller routes
app.Run();
