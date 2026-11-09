using SweetBakery.Extensions;
using SweetBakery.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddApplicationServices();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build application
var app = builder.Build();

// Error handling middleware - must be first
app.UseErrorHandling();

// Request logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();

// Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map Controllers
app.MapControllers();

// Run application
app.Run();