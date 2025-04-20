using Microsoft.EntityFrameworkCore;
using MLSM.Api.DBContext;
using MLSM.Api.Models;
using MLSM.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();

app.MapPost("/api/strings", async (LanguageStringEntity newString, AppDbContext db) =>
{
    var existingRecord = await db.MultilingualStrings
                                  .FirstOrDefaultAsync(x => x.Code == newString.Code);

    if (existingRecord != null)
        return Results.BadRequest(new GenericResponseApi
        {
            ResponseCode = "400",
            ResponseMessage = $"A record with Code '{newString.Code}' already exists."
        });

    db.MultilingualStrings.Add(newString);
    await db.SaveChangesAsync();

    return Results.Created($"/api/strings/{newString.Code}", new GenericResponseApi
    {
        ResponseCode = "201",
        ResponseMessage = "Success."
    });
});

app.MapGet("/api/strings", async (AppDbContext db) =>
{
    var data = await db.MultilingualStrings.ToListAsync();
    return Results.Ok(data);
});

app.MapPut("/api/strings/{code}", async (string code, LanguageStringEntity updatedString, AppDbContext db) =>
{
    var existingRecord = await db.MultilingualStrings
                                  .FirstOrDefaultAsync(x => x.Code == code);

    if (existingRecord is null)
    {
        return Results.BadRequest(new GenericResponseApi
        {
            ResponseCode = "404",
            ResponseMessage = $"No record found with Code '{code}'"
        });
    }
    if (code != updatedString.Code)
    {
        existingRecord = await db.MultilingualStrings.FirstOrDefaultAsync(x => x.Code == updatedString.Code);
        if (existingRecord is not null)
        {
            return Results.BadRequest(new GenericResponseApi
            {
                ResponseCode = "400",
                ResponseMessage = $"A record with Code '{updatedString.Code}' already exists."
            });

        }

        if (existingRecord is not null)
        {
            existingRecord.Code = updatedString.Code;
        }

    }
    if (existingRecord is not null)
    {
        existingRecord.TitleEn = updatedString.TitleEn;
        existingRecord.TitleAr = updatedString.TitleAr;
        existingRecord.Tags = updatedString.Tags;
    }

    await db.SaveChangesAsync();

    return Results.Created($"/api/strings/{code}", new GenericResponseApi
    {
        ResponseCode = "201",
        ResponseMessage = "Success."
    });
});

app.MapDelete("/api/strings/{code}", async (string code, AppDbContext db) =>
{
    var item = await db.MultilingualStrings.FirstOrDefaultAsync(x => x.Code == code);

    if (item is null)
    {
        return Results.BadRequest(new GenericResponseApi { ResponseCode = "404", ResponseMessage = $"No record found with Code '{code}'" });
    }

    db.MultilingualStrings.Remove(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/strings/{code}", new GenericResponseApi
    {
        ResponseCode = "201",
        ResponseMessage = "Success."
    });
});

app.Run();