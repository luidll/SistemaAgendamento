using Agendamento.Infrastructure.Data;
using Agendamento.WebApp.Components;
using Agendamento.WebApp.Services;
using Agendamento.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Agendamento.AuthCookie";
        options.LoginPath = "/";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddScoped<IDisponibilidadeService, DisponibilidadeService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

const string connectionString =
    "Server=localhost\\SQLEXPRESS;Database=AgendamentoDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";

builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
