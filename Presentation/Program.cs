
using Business.Services.Abstract;
using Business.Services.Concrete;
using Business.Utilities.EmailHandler.Abstract;
using Business.Utilities.EmailHandler.Concrete;
using Business.Utilities.EmailHandler.Models;
using Business.Utilities.Stripe;
using Common.Entities;
using Data;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"),x=>x.MigrationsAssembly("Data")));
builder.Services.AddIdentity<User,IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();



#region Data
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
#endregion

var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfiguration);
builder.Services.AddScoped<IEmailService, EmailService>();

#region Services
builder.Services.AddScoped<IProductService, Business.Services.Concrete.ProductService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
builder.Services.AddScoped<IMessageService, MessageService>();
#endregion
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
#region App
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "areas",
	 pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}"
	);

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Account}/{action=Register}"
);
app.MapControllerRoute(
	name: "product",
	pattern: "Product/Single/{id}",
	defaults: new { controller = "Product", action = "Single" });


using (var scope = app.Services.CreateScope())
{
	var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
	var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
	await DbInitializer.Seed(userManager, roleManager);
}
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Value;
app.Run();
#endregion