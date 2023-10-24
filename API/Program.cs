using API.Extensions;
using API.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Extension Method for our services located in folder Extensions
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

//Midlware
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("https://localhost:4200"));

//Same as going to a club where you show the id, it checks if it's a valid Token
app.UseAuthentication();
//But here it checks if you're above 18, coz you can't enter club if you're under 18
app.UseAuthorization();

app.MapControllers();

app.Run();
