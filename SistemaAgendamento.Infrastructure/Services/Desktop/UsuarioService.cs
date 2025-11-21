using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Application.Interfaces.Desktop;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Infrastructure.Services.Desktop
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IRotinaRepository _rotinaRepo;
        private readonly IMapper _mapper;

        public UsuarioService(
            IUsuarioRepository repo,
            IRotinaRepository rotinaRepo,
            IMapper mapper)
        {
            _repo = repo;
            _rotinaRepo = rotinaRepo;
            _mapper = mapper;
        }

        public async Task<List<UsuarioResponse>> ListarAsync()
        {
            var usuarios = await _repo.ListarAsync();
            return _mapper.Map<List<UsuarioResponse>>(usuarios);
        }

        public async Task<UsuarioRequest?> ObterParaEdicaoAsync(int id)
        {
            var usuario = await _repo.ObterPorIdAsync(id);
            return usuario == null ? null : _mapper.Map<UsuarioRequest>(usuario);
        }

        public async Task<string?> SalvarAsync(UsuarioRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Login))
                return "Login é obrigatório.";

            if (string.IsNullOrWhiteSpace(request.NomeCompleto))
                return "Nome é obrigatório.";

            if (await _repo.LoginExiste(request.Login, request.Id == 0 ? null : request.Id))
                return "Login já está sendo utilizado.";

            Usuario usuario;

            if (request.Id == 0)
            {
                if (request.Senha != request.ConfirmarSenha)
                    return "Senhas não conferem.";

                usuario = _mapper.Map<Usuario>(request);
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha!);

                await _repo.CriarAsync(usuario);
            }
            else
            {
                usuario = await _repo.ObterPorIdAsync(request.Id)
                          ?? throw new Exception("Usuário não encontrado.");

                _mapper.Map(request, usuario);

                if (!string.IsNullOrWhiteSpace(request.Senha))
                {
                    if (request.Senha != request.ConfirmarSenha)
                        return "Senhas não conferem.";

                    usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);
                }

                await _repo.AtualizarAsync(usuario);
            }

            return null;
        }

        public async Task ExcluirAsync(int id)
        {
            var usuario = await _repo.ObterPorIdAsync(id);
            if (usuario != null)
                await _repo.ExcluirAsync(usuario);
        }
        public async Task<UsuarioResponse?> ObterComPermissoesAsync(int id)
        {
            var usuario = await _repo.ObterComRotinasAsync(id);
            if (usuario == null)
                return null;

            return _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task ConcederRotinasAsync(int usuarioId, int[] rotinaIds)
        {
            var usuario = await _repo.ObterComRotinasAsync(usuarioId)
                ?? throw new Exception("Usuário não encontrado.");

            var rotinas = await _rotinaRepo.ObterPorIdsAsync(rotinaIds);

            foreach (var rotina in rotinas)
            {
                if (!usuario.RotinasPermitidas.Any(r => r.Id == rotina.Id))
                    usuario.RotinasPermitidas.Add(rotina);
            }

            await _repo.AtualizarAsync(usuario);
        }

        public async Task RevogarRotinasAsync(int usuarioId, int[] rotinaIds)
        {
            var usuario = await _repo.ObterComRotinasAsync(usuarioId)
                ?? throw new Exception("Usuário não encontrado.");

            usuario.RotinasPermitidas.ToList().RemoveAll(r => rotinaIds.Contains(r.Id));

            await _repo.AtualizarAsync(usuario);
        }
    }

}
