using Microsoft.AspNetCore.Authentication.Cookies;
using Solar.Services.Interfaces;
using Solar.Services.StaticServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
	options.Conventions.AuthorizeFolder("/Ekstern");
	options.Conventions.AuthorizeFolder("/Intern");
});

// Add data services
builder.Services.AddSingleton<IUsersDataService, EFCUserDataService>();
builder.Services.AddSingleton<IProjectDataService, EFCProjectDataService>();
builder.Services.AddSingleton<IRoofTypeDataService, EFCRoofTypeDataService>();
builder.Services.AddSingleton<IRoofMaterialDataService, EFCRoofMaterialDataService>();
builder.Services.AddSingleton<IDimensioningDataService, EFCDimensioningDataService>();
builder.Services.AddSingleton<IConsumptionCategoryDataService, EFCConsumptionCategoryDataService>();
builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
	options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
	options.LoginPath = "/Logind";
	options.AccessDeniedPath = "/Logind";
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("admin", policy => policy.RequireRole("admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
