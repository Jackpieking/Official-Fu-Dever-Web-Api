using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Persistence.SqlServer2016;

// Custom settings.
Console.OutputEncoding = Encoding.UTF8;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args: args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddPersistenceSqlServer2016(configuration: configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app
    .UseHttpsRedirection()
    .UseRouting();

app.MapControllers();

app.Run();
