using Microsoft.EntityFrameworkCore;
using SPJ_ProyectoMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using SPJ_ProyectoMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de DbContext para Identity
builder.Services.AddDbContext<DBContextSample>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBContextSampleConnection")));

// Configuraci�n de DbContext para SPJ_ProyectoMVCContext
builder.Services.AddDbContext<SPJ_ProyectoMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SPJ_ProyectoMVCContext")));

// Configuraci�n de Identity
builder.Services.AddDefaultIdentity<SampleUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<DBContextSample>();

// Agregar servicios de MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware de configuraci�n
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Importante para Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


