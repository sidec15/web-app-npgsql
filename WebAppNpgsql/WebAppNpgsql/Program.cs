using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using WebAppNpgsql.Context;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

//var lf = app.Services.GetRequiredService<ILoggerFactory>();

// Create a data source with the configuration you want:
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DbAppContext"));
await using var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<DbAppContext>(
  options => options
  .UseNpgsql(dataSource, o =>
  {
    o.UseNetTopologySuite();
  })
  .UseSnakeCaseNamingConvention()
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  var context = services.GetRequiredService<DbAppContext>();
  context.Database.EnsureCreated();
   DbInitializer.Initialize(context);
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
