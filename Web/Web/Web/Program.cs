using Abstracciones.Interfaces.Reglas;
using Autorizacion.Abstracciones.DA;
using Autorizacion.Abstracciones.Flujo;
using Autorizacion.DA;
using Autorizacion.DA.Repositorios;
using Autorizacion.Flujo;
using Autorizacion.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Reglas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();


builder.Services.AddScoped<IConfiguracion, Configuracion>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Seguridad/Login";
        options.LogoutPath = "/Seguridad/Logout";
        options.AccessDeniedPath = "/Seguridad/AccesoDenegado";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    });

builder.Services.AddTransient<IAutorizacionFlujo, AutorizacionFlujo>();
builder.Services.AddTransient<ISeguridadDA, SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper, RepositorioDapper>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.AutorizacionClaims();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
