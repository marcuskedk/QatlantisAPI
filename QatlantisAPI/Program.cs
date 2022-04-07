using Microsoft.EntityFrameworkCore;
using QatlantisAPI.models;

var builder = WebApplication.CreateBuilder(args);

// Set Content Root
builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

// Add services to the container.
builder.Services.AddDbContext<DnDContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DnD")));
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Cors
app.UseCors(options =>
{
    options.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin();
});

// Use Static Files
app.UseStaticFiles();

app.MapGet("/", async (DnDContext db) => await db.Customers.Include(e => e.Cases).ToListAsync());

app.MapGet("/api/cases", async (DnDContext db) => await db.Cases.ToListAsync());

app.MapGet("/api/customer", async (DnDContext db) => await db.Customers.Include(e => e.Cases).ToListAsync());

app.MapGet("/api/customer/{id}", async (DnDContext db, int id) => await db.Customers.FirstOrDefaultAsync(e => e.Id == id));

app.MapGet("/api/employee", async (DnDContext db) => await db.Employees.Include(e => e.Cases).ToListAsync());

app.Run();
