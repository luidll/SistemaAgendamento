using Agendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Agendamento.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

const string connectionString = "Server=localhost\\SQLEXPRESS;Database=AgendamentoDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();