using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IChatGPTBugFixService, ChatGPTBugFixService>();
    
    // Add CORS for Angular frontend
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngular",
            builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/api/bugfix/analyze", async (
    [FromBody] BugFixRequest request,
    IChatGPTBugFixService bugFixService) =>
{
    // Optional: Validate model manually if needed (since [ApiController] is not present)
    var validationContext = new ValidationContext(request, null, null);
    var validationResults = new List<ValidationResult>();
    if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    var result = await bugFixService.AnalyzeBugAsync(request);

    if (result.Status == "Success")
    {
        return Results.Ok(result);
    }

    return Results.Problem("Bug analysis failed", statusCode: 500);
})
.WithName("AnalyzeBugFix");

app.Run();


