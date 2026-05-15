using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using NailsStudio.Models.ViewModels;
using NailsStudio.Services.interfaces;
using NailsStudio.Models.Entities;

namespace NailsStudio.Areas.admin
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ServicosController : Controller
    {
        private readonly IServicoService _servicoService;
        public ServicosController (IServicoService service)
        {
            _servicoService = service;
        }

        public async Task<IActionResult> Index ()
        {
            var servicos = await _servicoService.ListarTodosAsync();
            return View(servicos);
        }
        

        public IActionResult Criar () => View (new ServicoViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar (ServicoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _servicoService.CriarAsync(model);
            TempData["Sucesso"] = "Serviço criado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar (int id)
        {
            var servico = await _servicoService.BuscarPorIdAsync(id);
            if (servico == null) return NotFound();

            var model = new ServicoViewModel
            {
                Id = servico.Id,
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                Preco = servico.Preco,
                DuracaoMinutos = servico.DuracaoMinutos,
                Ativo = servico.Ativo
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ServicoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _servicoService.EditarAsync(model);
            TempData["Sucesso"] = "Serviço atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Desativar (int id)
        {

            await _servicoService.DesativarAsync(id);
            TempData["Sucesso"] = "Serviço desativado.";
            return RedirectToAction(nameof(Index));
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reativar(int id)
        {
            await _servicoService.ReativarAsync(id);
            TempData["Sucesso"] = "Serviço reativado.";
            return RedirectToAction(nameof(Index));
        }
    }
}