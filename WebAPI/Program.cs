using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(setup => setup.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IKullaniciModelService, KullaniciModelService>();
builder.Services.AddScoped<IFirmaSahibiModelService, FirmaSahibiModelService>();
builder.Services.AddScoped<IParolaService, ParolaService>();
builder.Services.AddScoped<IYeniKullaniciModelService, YeniKullaniciModelService>();
builder.Services.AddScoped<ILoginModelService, LoginModelService>();
builder.Services.AddScoped<IIzinRepository, IzinRepository>();
builder.Services.AddScoped<IIzinModelService, IzinModelService>();
builder.Services.AddScoped<IAvansRepository, AvansRepository>();
builder.Services.AddScoped<IYeniAvansModelService, YeniAvansModelService>();
builder.Services.AddScoped<IHarcamaRepository, HarcamaRepository>();
builder.Services.AddScoped<IYeniHarcamaModelService, YeniHarcamaModelService>();
builder.Services.AddScoped<IAvansKisitlariModelService, AvansKisitlariModelService>();


builder.Services.AddIdentity<Kullanici, IdentityRole>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
}).AddDefaultTokenProviders()
	.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<Kullanici>>();

	await AppDbContextSeed.SeedAsync(appContext, rolemanager, usermanager);
}

app.Run();
