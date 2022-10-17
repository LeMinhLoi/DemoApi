using Microsoft.EntityFrameworkCore;
using Project.WebApi.Data;
using Project.WebApi.Helper;
using Project.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



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
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
