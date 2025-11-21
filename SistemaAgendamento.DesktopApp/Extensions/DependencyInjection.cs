using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using System;

// Usings para referenciar os assemblies necessários
using SistemaAgendamento.Application.Interfaces.Desktop; // Referência para as Interfaces (Desktop)
using SistemaAgendamento.Infrastructure.Services.Desktop;  // Referência para as Implementações (Desktop)
using SistemaAgendamento.Infrastructure.Repositories.Desktop; // Referência para os Repositórios (Desktop)
using SistemaAgendamento.Application.Mappings.Profiles.Desktop; // Referência para os Profiles (Desktop)


namespace SistemaAgendamento.DesktopApp.Extensions
{
    public static class DependencyInjection
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            var interfaceAssembly = typeof(IUsuarioService).Assembly;

            var implementationAssembly = typeof(UsuarioRepository).Assembly;

            services.AddAutoMapper(typeof(ModuloProfile).Assembly);

            Debug.WriteLine($"[DI] Interfaces buscadas em: {interfaceAssembly.GetName().Name}");
            Debug.WriteLine($"[DI] Implementações buscadas em: {implementationAssembly.GetName().Name}");


            var targetInterfaces = interfaceAssembly.GetExportedTypes()
                .Where(t => t.IsInterface &&
                           (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")))
                .ToList();

            var targetImplementations = implementationAssembly.GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (var interfaceType in targetInterfaces)
            {
                var implementationType = targetImplementations
                    .FirstOrDefault(c => interfaceType.IsAssignableFrom(c));

                if (implementationType != null)
                {
                    services.AddScoped(interfaceType, implementationType);
                    Debug.WriteLine($"[DI] Sucesso: {interfaceType.Name} -> {implementationType.Name}");
                }
                else
                {
                    Debug.WriteLine($"[DI] FALHA DE REGISTRO: Implementação não encontrada para {interfaceType.Name}");
                }
            }
        }
    }
}