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

app.MapPut("/api/cases/editstatus/{id}", async (DnDContext db, int id, Case cases) => 
{ 
    
});

app.MapPost("/api/cases", async (DnDContext db, Case cases) =>
{
    await db.Cases.AddAsync(cases);
    await db.SaveChangesAsync();

    return Results.Ok(cases);
});

app.MapPost("/api/customer", async (DnDContext db, Customer customers) =>
{
    await db.Customers.AddAsync(customers);
    await db.SaveChangesAsync();

    return Results.Ok(customers);
});

app.MapPost("/api/employee", async (DnDContext db, Employee employees) =>
{
    await db.Employees.AddAsync(employees);
    await db.SaveChangesAsync();

    return Results.Ok(employees);
});

app.MapDelete("/api/cases/{id}", async (DnDContext db, int id) =>
{
    var item = await db.Cases.FindAsync(id);
    if (item == null) return Results.NotFound();

    // File.Delete(builder.Environment.ContentRootPath + @"/wwwroot/" + item.Image);

    db.Cases.Remove(item);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/api/employee/{id}", async (DnDContext db, int id) =>
{
    var item = await db.Employees.FindAsync(id);
    if (item == null) return Results.NotFound();

    // File.Delete(builder.Environment.ContentRootPath + @"/wwwroot/" + item.Image);

    db.Employees.Remove(item);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/api/customer/{id}", async (DnDContext db, int id) =>
{
    var item = await db.Customers.FindAsync(id);
    if (item == null) return Results.NotFound();

    // File.Delete(builder.Environment.ContentRootPath + @"/wwwroot/" + item.Image);

    db.Customers.Remove(item);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/api/cases/{id}", async (DnDContext db, int id, Case cases) =>
{
    if (cases.Id != id) return Results.BadRequest();

    db.Cases.Update(cases);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapGet("/api/cases", async (DnDContext db) => await db.Cases.ToListAsync());

app.MapGet("/api/cases/{id}", async (DnDContext db, int id) => await db.Cases.FirstOrDefaultAsync(e => e.Id == id));

app.MapGet("/api/customer", async (DnDContext db) => await db.Customers.Include(e => e.Cases).ToListAsync());

app.MapGet("/api/customer/{id}", async (DnDContext db, int id) => await db.Customers.FirstOrDefaultAsync(e => e.Id == id));

app.MapGet("/api/employee", async (DnDContext db) => await db.Employees.Include(e => e.Cases).ToListAsync());

app.MapGet("/api/employee/{id}", async (DnDContext db, int id) => await db.Employees.FirstOrDefaultAsync(e => e.Id == id));

app.Run();
