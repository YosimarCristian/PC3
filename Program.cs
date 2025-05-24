using Microsoft.EntityFrameworkCore;
using PC3SALAZAR.Data;
using PC3SALAZAR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FeedbackContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
