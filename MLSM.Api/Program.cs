using Microsoft.EntityFrameworkCore;
using MLSM.Api.DBContext;
using MLSM.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();

app.MapGet("/api/strings", async (AppDbContext db) =>
{
    var data = await db.MultilingualStrings.ToListAsync();
    return Results.Ok(data);
});

app.MapDelete("/api/strings/{code}", async (string code, AppDbContext db) =>
{
    var item = await db.MultilingualStrings.FirstOrDefaultAsync(x => x.Code == code);

    if (item is null)
        return Results.NotFound($"No record found with Code = {code}");

    db.MultilingualStrings.Remove(item);
    await db.SaveChangesAsync();

    return Results.Ok($"Record with Code = {code} deleted.");
});

app.MapPut("/api/strings/{code}", async (string code, LanguageStringEntity updatedString, AppDbContext db) =>
{
    var existingRecord = await db.MultilingualStrings
                                  .FirstOrDefaultAsync(x => x.Code == code);

    if (existingRecord is null)
        return Results.NotFound($"No record found with Code = {code}");

    existingRecord.TitleEn = updatedString.TitleEn;
    existingRecord.TitleAr = updatedString.TitleAr;
    existingRecord.Tags = updatedString.Tags;

    await db.SaveChangesAsync();

    return Results.Ok($"Record with Code = {code} updated.");
});

app.Run();
