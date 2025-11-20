using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

// USINGS PARA REFERÊNCIA
using SistemaAgendamento.Application.Interfaces; // Ex: ISistemaService
using SistemaAgendamento.Application.Services;   // Ex: SistemaService (Agora aqui!)
using SistemaAgendamento.Infrastructure.Repositories; // Ex: SistemaRepository
using SistemaAgendamento.Application.Mappings.Profiles;

namespace SistemaAgendamento.DesktopApp.Extensions
{
    public static class DependencyInjection
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ModuloProfile).Assembly);

            var applicationAssembly = typeof(SistemaService).Assembly;

            var infrastructureAssembly = typeof(SistemaRepository).Assembly;

            Debug.WriteLine($"[DI] Services serão buscados em: {applicationAssembly.GetName().Name}");
            Debug.WriteLine($"[DI] Repositories serão buscados em: {infrastructureAssembly.GetName().Name}");

            var serviceImplementations = applicationAssembly.GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
                .ToList();

            foreach (var impl in serviceImplementations)
            {
                var iface = impl.GetInterface($"I{impl.Name}");
                if (iface != null)
                {
                    services.AddScoped(iface, impl);
                    Debug.WriteLine($"[DI] Service Registrado: {iface.Name} -> {impl.Name}");
                }
            }
            var repoImplementations = infrastructureAssembly.GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
                .ToList();

            foreach (var impl in repoImplementations)
            {
                var iface = impl.GetInterface($"I{impl.Name}");

                if (iface == null)
                {
                    iface = applicationAssembly.GetExportedTypes()
                       .FirstOrDefault(i => i.IsInterface && i.Name == $"I{impl.Name}");
                }

                if (iface != null)
                {
                    services.AddScoped(iface, impl);
                    Debug.WriteLine($"[DI] Repository Registrado: {iface.Name} -> {impl.Name}");
                }
            }
        }
    }
}