using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Application.Interfaces.Repositories;
using SistemaAgendamento.Application.Mappings;
using SistemaAgendamento.Application.Mappings.Profiles;
using SistemaAgendamento.Application.Services;
using SistemaAgendamento.Infrastructure.Data;
using SistemaAgendamento.Infrastructure.Repositories;

namespace SistemaAgendamento.DesktopApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            //Register Services and Repositories
            #region REPOSITORIES
            builder.Services.AddScoped<IModuloRepository, ModuloRepository>();
            builder.Services.AddScoped<IRotinaRepository, RotinaRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            #endregion
            #region SERVICES
            builder.Services.AddScoped<IModuloService, ModuloService>();
            builder.Services.AddScoped<IRotinaService, RotinaService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            #endregion
            #region MAPPINGS
            builder.Services.AddAutoMapper(typeof(ModuloProfile));
            builder.Services.AddAutoMapper(typeof(SistemaProfile));
            builder.Services.AddAutoMapper(typeof(UsuarioProfile), typeof(RotinaProfile));

            #endregion


            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            const string connectionString = "Server=localhost\\SQLEXPRESS;Database=AgendamentoDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );



            return builder.Build();
        }
    }
}