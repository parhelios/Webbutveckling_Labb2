using DataAccess;
using Microsoft.EntityFrameworkCore;
using StoreApp.Api.Extensions;
using StoreApp.Shared.Interfaces;
using StoreApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StoreDbContext>(
	options => options.UseSqlServer(connectionString)
	);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.MapUserEndpoints();
app.MapCategoryEndpoints();
app.MapCustomerInteractionEndpoints();
app.MapProductEndpoints();

app.Run();
