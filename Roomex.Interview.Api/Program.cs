using Microsoft.OpenApi.Models;
using Roomex.Interview.Api.Filters;
using Roomex.Interview.Core.Services;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Logging.AddConsole();

builder.Services.AddServices();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Distance API",
        Description = "An ASP.NET Core Web API for calculating the distance between two cities",
        Contact = new OpenApiContact
        {
            Name = "Nicolae Ceobotaru",
            Email = "nicolae.ceobotaru@gmail.com"
        }
    });
    options.OperationFilter<SwaggerAcceptLanguageHeaderFilter>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Logger.LogInformation("Starting the app");

app.Run();

public partial class Program { }
