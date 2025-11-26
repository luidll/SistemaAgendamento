using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SistemaAgendamento.DesktopApp.Extensions;
using SistemaAgendamento.Infrastructure.Data;
using System.Reflection;
using System.Windows.Forms;

namespace SistemaAgendamento.DesktopApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            var a = Assembly.GetExecutingAssembly();
            var resourceName = "SistemaAgendamento.DesktopApp.appsettings.json";

            using var stream = a.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new Exception($"Não foi possível encontrar o arquivo '{resourceName}'. Verifique se a Build Action está como Embedded Resource.");
            }
            var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
            builder.Configuration.AddConfiguration(config);
            builder
              .UseMauiApp<App>()
              .ConfigureFonts(fonts =>
              {
                  fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
              });
            #region
            builder.Services.RegisterApplicationServices();
            #endregion
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            return builder.Build();
        }
    }
}