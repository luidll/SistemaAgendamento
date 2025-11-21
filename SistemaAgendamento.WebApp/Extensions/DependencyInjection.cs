
using SistemaAgendamento.Application.Interfaces.Desktop;
using SistemaAgendamento.Infrastructure.Repositories.Desktop;
using SistemaAgendamento.Application.Mappings.Profiles.Desktop;
using System.Diagnostics;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Infrastructure.Repositories.Web;
using SistemaAgendamento.Application.Mappings.Profiles;


namespace SistemaAgendamento.WebApp.Extensions
{
    public static class DependencyInjection
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            var interfaceAssembly = typeof(IAgendamentoRepository).Assembly;

            var implementationAssembly = typeof(AgendamentoRepository).Assembly;

            services.AddAutoMapper(typeof(AgendamentoProfile).Assembly);

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