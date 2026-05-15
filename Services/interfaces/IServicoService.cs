using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsStudio.Models.Entities;
using NailsStudio.Models.ViewModels;

namespace NailsStudio.Services.interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<Servico>> ListarTodosAsync();
        Task <Servico?> BuscarPorIdAsync (int id);
        Task CriarAsync(ServicoViewModel model);
        Task EditarAsync(ServicoViewModel model);
        Task DesativarAsync(int id);
        Task ReativarAsync(int id);
    }
}