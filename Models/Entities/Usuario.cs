using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NailsStudio.Models.Entities
{
    public class Usuario : IdentityUser
    {
        public String NomeCompleto {get;set;} = string.Empty;
        public ICollection<Agendamento> Agendamentos {get;set;} = [];
    }
}