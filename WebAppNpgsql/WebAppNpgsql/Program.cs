using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebAppNpgsql.Context;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAutoMapper(typeof(Program));

var connStr = configuration.GetConnectionString("DbAppContext");

// NEW STYLE
// Create a data source with the configuration you want:
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connStr);
dataSourceBuilder.UseNetTopologySuite();
await using var dataSource = dataSourceBuilder.Build();

services.AddDbContext<DbAppContext>(
  options => options
  .UseNpgsql(dataSource, o =>
  {
    o.UseNetTopologySuite();
  })
  .UseSnakeCaseNamingConvention()
);

// OLD STYLE
//services.AddDbContext<DbAppContext>(
//  options => options
//  .UseNpgsql(connStr, o =>
//  {
//    o.UseNetTopologySuite();
//  })
//  .UseSnakeCaseNamingConvention()
//);

// Add Hangfire services.
services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(connStr)
    );

// Add the processing server as IHostedService
services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
  var serviceProvider = scope.ServiceProvider;

  var context = serviceProvider.GetRequiredService<DbAppContext>();
  context.Database.IsRelational();
  DbInitializer.Initialize(context);
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
