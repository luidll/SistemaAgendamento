using Agendamento.Core.Entities;
using Agendamento.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace Agendamento.WebApp.Components.Pages
{
    [Authorize(Roles = "Cadastro de Salas")]
    public partial class Salas : ComponentBase
    {
        [Inject]
        private ISalaService SalaService { get; set; } = null!;

        private Sala salaAtual { get; set; } = new Sala();
        private List<Sala> listaSalas = new();
        private string? mensagemStatus;

        protected override async Task OnInitializedAsync()
        {
            await LoadSalas();
        }

        protected async Task LoadSalas()
        {
            listaSalas = await SalaService.GetAllAsync();
        }

        protected async Task SalvarSala()
        {
            mensagemStatus = null;
            if (string.IsNullOrWhiteSpace(salaAtual.Nome) || salaAtual.Capacidade <= 0)
            {
                mensagemStatus = "O nome e a capacidade são obrigatórios.";
                return;
            }

            try
            {
                await SalaService.AddOrUpdateAsync(salaAtual);
                mensagemStatus = $"Sala '{salaAtual.Nome}' salva com sucesso.";
                LimparFormulario();
                await LoadSalas();
            }
            catch (Exception ex)
            {
                mensagemStatus = $"Erro: {ex.Message}";
            }
        }

        protected async Task DeletarSala(int id)
        {
            await SalaService.DeleteAsync(id);
            await LoadSalas();
        }

        protected void CarregarParaEdicao(Sala sala)
        {
            salaAtual = new Sala
            {
                Id = sala.Id,
                Nome = sala.Nome,
                Capacidade = sala.Capacidade,
                Localizacao = sala.Localizacao,
                Descricao = sala.Descricao,
                ImagemUrl = sala.ImagemUrl
            };
        }

        protected void LimparFormulario()
        {
            salaAtual = new Sala();
            mensagemStatus = null;
        }
    }
}