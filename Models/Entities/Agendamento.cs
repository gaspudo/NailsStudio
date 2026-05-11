using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsStudio.Models.Entities
{
    public enum StatusAgendamento
    {
        Pendente,
        Confirmado,
        Cancelado
    }
    public class Agendamento
    {
        public int Id {get;set;}
        public string UsuarioId {get;set;} = string.Empty;
        public Usuario? Usuario {get;set;} 

        public int ServicoId {get;set;}
        
        public DateTime DataHora {get;set;}
        public StatusAgendamento Status {get;set;} = StatusAgendamento.Pendente;
        public DateTime CriadoEm {get;set;} = DateTime.UtcNow;

    }
}