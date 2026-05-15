using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NailsStudio.Models.ViewModels
{
    public class ServicoViewModel
    {
        public int Id {get;set;}

        [Required(ErrorMessage ="Nome obrigatório")]
        public string Nome {get;set;} = string.Empty;

        [Required(ErrorMessage ="Preço obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage ="O serviço precisa ter um preço maior que zero")]
        public decimal Preco {get;set;}

        [MaxLength(500)]
        public string? Descricao {get;set;}

        [Required(ErrorMessage ="Duração obrigatória")]
        [Range(30, 200, ErrorMessage ="Duração entre 30 e 200 minutos")]
        public int DuracaoMinutos {get;set;}

        public bool Ativo {get;set;} = true;
    }
}