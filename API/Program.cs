using System.Text.Json.Serialization;
using API.GraphQL;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Impl;
using Core.DomainServices.Services.Intf;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MainDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
});
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICanteenRepo, CanteenEFRepo>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeEFRepo>();
builder.Services.AddScoped<IPackageRepo, PackageEFRepo>();
builder.Services.AddScoped<IProductRepo, ProductEFRepo>();
builder.Services.AddScoped<IStudentRepo, StudentEFRepo>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<Query>();
builder.Services.AddScoped<GraphQLTypes>();
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<GraphQLTypes>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGraphQL();

app.MapControllers();

app.Run();
