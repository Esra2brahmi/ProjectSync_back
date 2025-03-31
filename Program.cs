using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using projectSync_back.data;
using projectSync_back.Interfaces;
using projectSync_back.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Everything", policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin, for testing purposes
              .AllowAnyHeader()  // Allow any headers
              .AllowAnyMethod(); // Allow any HTTP method
    });
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add DbContext with PostgreSQL connection
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProjectRepository,ProjectRepository>();
builder.Services.AddScoped<ITaskRepository,ProjectTaskRepository>();



var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("Everything"); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.Run();


