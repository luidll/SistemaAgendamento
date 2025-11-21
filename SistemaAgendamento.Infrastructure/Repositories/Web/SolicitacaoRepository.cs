using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;
using SistemaAgendamento.Application.Interfaces.Web;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAgendamento.Infrastructure.Repositories.Web
{
    public class SolicitacaoRepository : ISolicitacaoRepository
    {
        private readonly AppDbContext _db;

        public SolicitacaoRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Solicitacao solicitacao)
        {
            _db.Solicitacoes.Add(solicitacao);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Solicitacao solicitacao)
        {
            _db.Solicitacoes.Update(solicitacao);
            await _db.SaveChangesAsync();
        }

        public async Task<Solicitacao?> GetByIdAsync(int id)
        {
            return await _db.Solicitacoes
                .Include(s => s.Agendamento)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Solicitacao>> GetRecebidasAsync(int usuarioId)
        {
            return await _db.Solicitacoes
                .AsNoTracking()
                .Include(s => s.Solicitante)
                .Include(s => s.Agendamento)
                    .ThenInclude(a => a.Sala)
                .Include(s => s.Agendamento)
                    .ThenInclude(a => a.Usuario)
                .Where(s => s.SolicitadoId == usuarioId)
                .OrderByDescending(s => s.DataSolicitacao)
                .ToListAsync();
        }

        public async Task<List<Solicitacao>> GetEnviadasAsync(int usuarioId)
        {
            return await _db.Solicitacoes
                .AsNoTracking()
                .Include(s => s.Solicitado)
                .Include(s => s.Agendamento)
                    .ThenInclude(a => a.Sala)
                .Where(s => s.SolicitanteId == usuarioId)
                .OrderByDescending(s => s.DataSolicitacao)
                .ToListAsync();
        }
    }
}