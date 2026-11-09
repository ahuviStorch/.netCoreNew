using SweetBakery.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddApplicationServices();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseApplicationMiddleware();

// Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Controllers
app.MapControllers();

app.Run();