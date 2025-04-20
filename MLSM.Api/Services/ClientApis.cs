using Microsoft.EntityFrameworkCore;
using MLSM.Api.DBContext;

namespace MLSM.Api.Services
{
    public static class ClientApis
    {
        public static void MapClientApis(this IEndpointRouteBuilder client)
        {

            client.MapGet("/api/lastmodified", async (AppDbContext db) =>
            {
                var data = await db.MultilingualStrings.OrderByDescending(x => x.LastUpdateTimeStamp).FirstOrDefaultAsync();
                var response = data?.LastUpdateTimeStamp.HasValue == true ? data.LastUpdateTimeStamp.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") : null;
                return Results.Ok(new { DateTimeStamp = response });
            });

            client.MapGet("/api/filteredstrings/{filter}", async (string filter, AppDbContext db) =>
            {
                if (!DateTime.TryParseExact(filter, "yyyy-MM-dd HH:mm:ss.fff", null, System.Globalization.DateTimeStyles.None, out var sinceFilter))
                {
                    return Results.BadRequest("Invalid date format. Use 'yyyy-MM-dd HH:mm:ss.fff'");
                }
                var data = await db.MultilingualStrings.Where(x => x.LastUpdateTimeStamp.HasValue && x.LastUpdateTimeStamp.Value > sinceFilter).Select(x => new { x.Code, x.TitleEn, x.TitleAr }).ToListAsync();
                return Results.Ok(data);
            });

            client.MapGet("/api/strings", async (AppDbContext db) =>
            {
                var data = await db.MultilingualStrings.Select(x => new { x.Code, x.TitleEn, x.TitleAr }).ToListAsync();
                return Results.Ok(data);
            });
        }
    }
}
