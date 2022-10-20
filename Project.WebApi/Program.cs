using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.WebApi.Data;
using Project.WebApi.Helper;
using Project.WebApi.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }) ;

//for mysql

//var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
//// Replace 'YourDbContext' with the name of your own DbContext derived class.
//services.AddDbContext<DataContext>(
//    dbContextOptions => dbContextOptions
//        .UseMySql(connectionString, serverVersion)
//        .LogTo(Console.WriteLine, LogLevel.Information)
//        .EnableSensitiveDataLogging()
//        .EnableDetailedErrors()
//);


builder.Services.AddTransient<IWalkDifficultyRepository, WalkDifficultyRepository>();
builder.Services.AddTransient<IRegionRepository, RegionRepository>();
builder.Services.AddTransient<IWalkRepository, WalkRepository>();
builder.Services.AddTransient<ITokenHandler, Project.WebApi.Repositories.TokenHandler>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
