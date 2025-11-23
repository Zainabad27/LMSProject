using LmsApp2.Api;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Middlewares;
using LmsApp2.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Injecting Dependencies
builder.Services.AddDI();


// Connecting to Database.

builder.Services.AddDbContext<LmsDatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddExceptionHandler<AppExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/v1/Employees/AddEmployee"), appBuilder =>
{
    appBuilder.UseMiddleware<IsAdmin>();
});


app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
