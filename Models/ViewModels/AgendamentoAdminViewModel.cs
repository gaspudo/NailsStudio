using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsStudio.Models.ViewModels
{
    public class AgendamentoAdminViewModel
    {
        public int Id {get;set;}
        public string NomeCliente {get;set;} = string.Empty;
        public string EmailCliente {get;set;} = string.Empty;
        public string NomeServico {get;set;} = string.Empty;
        public decimal PrecoServico {get;set;}
        public DateTime DataHora {get;set;}
        public string Status {get;set;} = string.Empty;
        public DateTime CriadoEm {get;set;}
    }
}