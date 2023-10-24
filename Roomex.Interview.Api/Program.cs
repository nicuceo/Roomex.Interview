using Roomex.Interview.Api.Filters;
using Roomex.Interview.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
