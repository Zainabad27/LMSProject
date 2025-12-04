using LmsApp2.Api;
using LmsApp2.Api.Exceptions;
//using LmsApp2.Api.Middlewares;
using LmsApp2.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Logging.AddFilter("Microsoft.AspNetCore.Watch", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft", LogLevel.Warning);

builder.Logging.AddFilter("Default", LogLevel.Warning);







// setting IForm Upload options

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartHeadersLengthLimit = 1024 * 1024; // 1 MB
});




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





// Connecting to Database.

builder.Services.AddDbContext<LmsDatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var JwtIssuer = builder.Configuration["AppSettingsForJWT:Issuer"];
var JwtKey = builder.Configuration["AppSettingsForJWT:Token"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtIssuer,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey!))
    };


    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["AccessToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            // Skip default logic
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync("{\"error\": \"You are not authenticated. Token missing or invalid(Expired).\"}");
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync("{\"error\": \"UnAuthorized Access,You are not allowed to access this resource.\"}");
        }
    };
});


// Injecting Dependencies

builder.Services.AddDI();

builder.Services.AddExceptionHandler<AppExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseExceptionHandler(_ => { });

//app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/v1/Employees/AddEmployee"), appBuilder =>
//{
//    appBuilder.UseMiddleware<IsAdmin>();
//});


app.UseRouting();
//app.UseAuthentication();

//app.UseWhen(context =>
//    !context.Request.Path.StartsWithSegments("/api/v1/Auth/Login"),
//    appBuilder =>
//    {
//        appBuilder.UseMiddleware<JwtVerify>();
//    });


app.UseAuthentication();


//app.UseMiddleware<JwtVerify>(); // your custom JWT validation
app.UseAuthorization();



app.UseHttpsRedirection();


app.MapControllers();


app.StartupLog();

app.Run();
