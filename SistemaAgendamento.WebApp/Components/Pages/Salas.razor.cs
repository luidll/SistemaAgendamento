using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    [Authorize(Roles = "Cadastro de Salas")]
    public partial class Salas : ComponentBase
    {
        [Inject]
        private ISalaService SalaService { get; set; } = null!;

        private SalaRequest salaAtual = new();
        private List<SalaResponse> listaSalas = new List<SalaResponse>();
        private string? mensagemStatus;

        protected override async Task OnInitializedAsync()
        {
            await LoadSalas();
        }

        private async Task LoadSalas()
        {
            listaSalas = await SalaService.GetAllAsync();
        }

        private async Task SalvarSala()
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
                mensagemStatus = $"Erro ao salvar: {ex.Message}";
            }
        }

        private async Task DeletarSala(int id)
        {
            try
            {
                await SalaService.DeleteAsync(id);
                await LoadSalas();
            }
            catch (Exception ex)
            {
                mensagemStatus = $"Erro ao excluir: {ex.Message}";
            }
        }

        private void CarregarParaEdicao(SalaResponse sala)
        {
            salaAtual = new SalaRequest
            {
                Id = sala.Id,
                Nome = sala.Nome,
                Capacidade = sala.Capacidade,
                Localizacao = sala.Localizacao,
                Descricao = sala.Descricao,
                ImagemUrl = sala.ImagemUrl
            };
        }

        private void LimparFormulario()
        {
            salaAtual = new SalaRequest();
            mensagemStatus = null;
        }
    }
}
