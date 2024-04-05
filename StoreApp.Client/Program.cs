using MudBlazor.Services;
using StoreApp.Client.Components;
using StoreApp.Client.Services;
using StoreApp.Shared.Dtos.ProductDtos;
using StoreApp.Shared.Dtos.UserDtos;
using StoreApp.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();

builder.Services.AddHttpClient("storeApp", client =>
{
    client.BaseAddress = new Uri("https://localhost:7175/");
});

builder.Services.AddSingleton<IProductService<ProductDto>, ProductService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<ICategoryService<ProductCategoryDto>, CategoryService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IShoppingCartService<ProductDto>, ShoppingCartService>();
builder.Services.AddSingleton<IUserService<UserDto>, UserService>();
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
