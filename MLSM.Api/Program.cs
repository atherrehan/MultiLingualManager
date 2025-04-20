using Microsoft.EntityFrameworkCore;
using MLSM.Api.DBContext;
using MLSM.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

app.MapClientApis();

app.MapServerApis();

app.Run();