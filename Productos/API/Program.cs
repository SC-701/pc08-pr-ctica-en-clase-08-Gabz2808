using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.TipoCambio;
using Abstracciones.Modelos;
using Autorizacion.Middleware;
using DA;
using DA.Repositorios;
using Flujos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Reglas;
using Servicios;
using System.Text;
using Autorizacion.Abstracciones.Flujo;
using Autorizacion.Abstracciones.DA;
using Autorizacion.DA;
using Abstracciones.Interfaces.DA;

var builder = WebApplication.CreateBuilder(args);

var tokenConfiguration = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>()
    ?? throw new InvalidOperationException("Falta la sección Token en la configuración.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfiguration.Issuer,
            ValidAudience = tokenConfiguration.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.key))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string politicaCors = "WebProductos";
builder.Services.AddCors(options =>
{
    options.AddPolicy(politicaCors, policy =>
    {
        policy.WithOrigins(
                "https://localhost:7206",
                "http://localhost:5268",
                "https://localhost:7023",
                "http://localhost:5126")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();
builder.Services.AddScoped<IProductoReglas, ProductoReglas>();
builder.Services.AddScoped<IProductoDA, ProductoDA>();
builder.Services.AddScoped<Abstracciones.Interfaces.DA.IRepositorioDapper, RepositorioDapper>();
builder.Services.AddHttpClient<ITipoCambioServicio, TipoCambioServicio>();
builder.Services.AddScoped<ITipoCambioServicio, TipoCambioServicio>();
builder.Services.AddScoped<ICategoriaFlujo, CategoriaFlujo>();
builder.Services.AddScoped<ICategoriaDA, CategoriaDA>();

builder.Services.AddScoped<ISubCategoriaFlujo, SubCategoriaFlujo>();
builder.Services.AddScoped<ISubCategoriaDA, SubCategoriaDA>();

builder.Services.AddTransient<IAutorizacionFlujo, Autorizacion.Flujo.AutorizacionFlujo>();
builder.Services.AddTransient<ISeguridadDA, SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper, Autorizacion.DA.Repositorios.RepositorioDapper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(politicaCors);
app.UseAuthentication();
app.AutorizacionClaims();
app.UseAuthorization();

app.MapControllers();

app.Run();
