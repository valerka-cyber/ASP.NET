using Intro.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Intro.DAL.Context.IntroContext>
    (options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("introDb")));
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<RandomService>();
builder.Services.AddSingleton<IHasher, ShaHasher>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
