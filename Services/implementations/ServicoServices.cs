using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsStudio.Services.interfaces;
using Microsoft.EntityFrameworkCore;
using NailsStudio.Data;
using NailsStudio.Models.Entities;
using NailsStudio.Models.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NailsStudio.Services.implementations
{
    public class ServicoServices : IServicoService
    {
        private readonly DbContexto _context;

        public ServicoServices (DbContexto contexto)
        {
            _context = contexto;
        }
        public async Task<Servico?> BuscarPorIdAsync(int id) 
        => await _context.Servicos.FindAsync(id);

        public async Task CriarAsync (ServicoViewModel model)
        {
            try {
            var servico = new Servico
            {
                Nome = model.Nome,
                Descricao = model.Descricao,
                Preco = model.Preco,
                DuracaoMinutos = model.DuracaoMinutos,
                Ativo = true
            };
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception ($"{ex.Message}");
            }
        }

        public async Task DesativarAsync(int id)
        {
            try{
                var servico = await _context.Servicos.FindAsync(id) ?? 
                throw new InvalidOperationException ($"Serviço não encontrado.");
                
                
                servico.Ativo = false;
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception ($"{ex.Message}");
            }
        }

        public async Task EditarAsync(ServicoViewModel model)
        {
            var servico = await _context.Servicos.FindAsync(model.Id) ?? 
            throw new InvalidOperationException( $"Serviço {model.Id} não foi encontrado.");

            servico.Nome = model.Nome;
            servico.Descricao = model.Descricao;
            servico.Preco = model.Preco;
            servico.DuracaoMinutos = model.DuracaoMinutos;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Servico>> ListarTodosAsync() 
        => await _context.Servicos.OrderBy(s => s.Nome).ToListAsync();   
    
        public async Task ReativarAsync(int id)
        {
            var servico = await _context.Servicos.FindAsync(id)
                ?? throw new InvalidOperationException($"Serviço {id} não encontrado.");

            servico.Ativo = true;
            await _context.SaveChangesAsync();
        }
    }
    
}