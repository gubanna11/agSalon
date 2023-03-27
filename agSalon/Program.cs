using agSalon.Domain.Abstract;
using agSalon.Domain.Abstract.Repositories;
using agSalon.Domain.Concrete;
using agSalon.Domain.Entities;
using agSalon.Services.Services.Implementations;
using agSalon.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddEntityFrameworkMySql().AddDbContext<AppDbContext>(options => {
	options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnectionString"),
		new MySqlServerVersion(new Version(8, 0, 11)));
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddScoped<IEntityBaseRepository<GroupOfServices>, IGroupsService>();
//builder.Services.AddScoped<EntityBaseRepository<GroupOfServices>, GroupsService>();

builder.Services.AddScoped<IGroupsService, GroupsService>();
builder.Services.AddScoped<IServicesService, ServicesService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.Seed(app);

app.Run();
