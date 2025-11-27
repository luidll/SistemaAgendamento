using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Domain.Enums;
using AutoMapper;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.Interfaces.Desktop;

namespace SistemaAgendamento.Infrastructure.Services.Web
{
    public class SolicitacaoService : ISolicitacaoService
    {
        private readonly ISolicitacaoRepository _solicitacaoRepo;
        private readonly IAgendamentoRepository _agendamentoRepo;
        private readonly IMapper _mapper;

        public SolicitacaoService(
            ISolicitacaoRepository solicitacaoRepo,
            IAgendamentoRepository agendamentoRepo,
            IMapper mapper)
        {
            _solicitacaoRepo = solicitacaoRepo;
            _agendamentoRepo = agendamentoRepo;
            _mapper = mapper;
        }

        public async Task CriarSolicitacaoAsync(SolicitacaoRequest request, int usuarioSolicitanteId)
        {
            var agendamento = await _agendamentoRepo.GetAgendamentoByIdAsync(request.AgendamentoId);

            if (agendamento == null) throw new Exception("Agendamento não encontrado.");
            if (agendamento.UsuarioId == usuarioSolicitanteId) throw new Exception("Você não pode solicitar seu próprio horário.");

            var novaSolicitacao = new Solicitacao
            {
                SolicitanteId = usuarioSolicitanteId,
                SolicitadoId = agendamento.UsuarioId,
                AgendamentoId = request.AgendamentoId,
                Justificativa = request.Justificativa,
                DataSolicitacao = DateTime.Now,
                DataHoraInicioSolicitada = request.DataHoraInicioSolicitada,
                DataHoraFimSolicitada = request.DataHoraFimSolicitada,
                Status = StatusSolicitacao.Pendente
            };

            await _solicitacaoRepo.AddAsync(novaSolicitacao);
        }

        public async Task AceitarSolicitacaoAsync(int solicitacaoId, int usuarioLogadoId)
        {
            var solicitacao = await _solicitacaoRepo.GetByIdWithAgendamentoAsync(solicitacaoId);
            if (solicitacao == null) throw new Exception("Solicitação não encontrada.");

            if (solicitacao.SolicitadoId != usuarioLogadoId) throw new Exception("Apenas o destinatário pode aceitar.");

            var agendamentoOriginal = solicitacao.Agendamento ??
                throw new Exception("Agendamento original não carregado.");

            var inicioOriginal = agendamentoOriginal.DataHoraInicio;
            var fimOriginal = agendamentoOriginal.DataHoraFim;

            var inicioSolicitado = solicitacao.DataHoraInicioSolicitada;
            var fimSolicitado = solicitacao.DataHoraFimSolicitada;

            if (inicioSolicitado >= fimSolicitado) throw new Exception("Horário de início deve ser anterior ao horário de fim.");
            if (inicioSolicitado < inicioOriginal || fimSolicitado > fimOriginal)
                throw new Exception("Solicitação deve estar contida no intervalo do agendamento original.");

            var novoAgendamento = new Agendamento
            {
                Titulo = agendamentoOriginal.Titulo,
                Descricao = $"Cedido via solicitação #{solicitacao.Id}: {solicitacao.Justificativa}",
                UsuarioId = solicitacao.SolicitanteId,
                SalaId = agendamentoOriginal.SalaId,
                DataHoraInicio = inicioSolicitado,
                DataHoraFim = fimSolicitado
            };

            await _agendamentoRepo.AddAsync(novoAgendamento);

            if (inicioSolicitado > inicioOriginal)
            {
                var parteInicial = new Agendamento
                {
                    Titulo = agendamentoOriginal.Titulo,
                    Descricao = agendamentoOriginal.Descricao,
                    UsuarioId = agendamentoOriginal.UsuarioId,
                    SalaId = agendamentoOriginal.SalaId,
                    DataHoraInicio = inicioOriginal,
                    DataHoraFim = inicioSolicitado
                };
                await _agendamentoRepo.AddAsync(parteInicial);
            }

            if (fimSolicitado < fimOriginal)
            {
                var parteFinal = new Agendamento
                {
                    Titulo = agendamentoOriginal.Titulo,
                    Descricao = agendamentoOriginal.Descricao,
                    UsuarioId = agendamentoOriginal.UsuarioId,
                    SalaId = agendamentoOriginal.SalaId,
                    DataHoraInicio = fimSolicitado,
                    DataHoraFim = fimOriginal
                };
                await _agendamentoRepo.AddAsync(parteFinal);
            }

            await _agendamentoRepo.DeleteAsync(agendamentoOriginal.Id);

            solicitacao.Status = StatusSolicitacao.Aprovada;
            solicitacao.Finalizado = true;
            solicitacao.RespostaObservacao = $"Aprovada por usuário {solicitacao.Agendamento.Usuario.NomeCompleto} em {DateTime.Now}";
            await _solicitacaoRepo.UpdateAsync(solicitacao);
        }


        public async Task RecusarSolicitacaoAsync(int solicitacaoId, int usuarioLogadoId)
        {
            var solicitacao = await _solicitacaoRepo.GetByIdAsync(solicitacaoId);
            if (solicitacao == null) return;

            if (solicitacao.SolicitadoId != usuarioLogadoId) throw new Exception("Apenas o destinatário pode recusar.");

            solicitacao.Status = StatusSolicitacao.Recusada;
            solicitacao.Finalizado = true;
            await _solicitacaoRepo.UpdateAsync(solicitacao);
        }

        public async Task<List<SolicitacaoResponse>> GetRecebidasAsync(int usuarioId)
        {
            var lista = await _solicitacaoRepo.GetRecebidasAsync(usuarioId);
            return _mapper.Map<List<SolicitacaoResponse>>(lista);
        }

        public async Task<List<SolicitacaoResponse>> GetEnviadasAsync(int usuarioId)
        {
            var lista = await _solicitacaoRepo.GetEnviadasAsync(usuarioId);
            return _mapper.Map<List<SolicitacaoResponse>>(lista);
        }
        public async Task<List<SolicitacaoResponse>> GetFinalizadasAsync(int usuarioId)
        {
            var lista = await _solicitacaoRepo.GetFinalizadasAsync(usuarioId);
            return _mapper.Map<List<SolicitacaoResponse>>(lista);
        }

    }
}